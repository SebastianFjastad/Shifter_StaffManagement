using System;
using System.Linq;
using Framework;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;
using Shifter.Service.Utilities;

namespace Shifter.Service.Services
{
    public class SettingsService : ShifterServiceBase, ISettingsService
    {
        private readonly ISettingsDomainService settingsDomainService;
        private readonly IShiftService shiftService;

        public SettingsService(ISettingsDomainService settingsDomainService, IShiftService shiftService)
        {
            Guard.ArgumentNotNull(settingsDomainService, "settingsDomainService");
            Guard.ArgumentNotNull(shiftService, "shiftService");

            this.settingsDomainService = settingsDomainService;
            this.shiftService = shiftService;
        }

        public LoadSettingsResponse LoadSettingsByRestaurant(LoadSettingsByRestaurantRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new LoadSettingsResponse();

                var settings = settingsDomainService.LoadSettings(request.RestaurantId);

                if (settings.IsNotNull())
                {
                    result.Settings = MappingUtility.Map<Settings, SettingsDto>(settings);
                }
                else
                {
                    result.NotificationCollection.AddError("No settings found.");
                }

                return result;
            });
        }

        public GenericServiceResponse SaveSettings(SaveSettingsRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var settings = MappingUtility.Map<SettingsDto, Settings>(request.Settings);

                var notificationCollection = settingsDomainService.SaveSettings(settings);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse LoadRestaurantNotifications(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notifications = NotificationCollection.CreateEmpty();

                var settings = settingsDomainService.LoadSettings(request.EntityId);

                if (settings.IsNotNull())
                {
                    var toDate = DateTime.Today.AddDays(settings.UnassignedShiftNotificationPeriod);

                    var loadShiftRequest = new LoadShiftsRequest()
                    {
                        FromDate = DateTime.Today,
                        ToDate = toDate,
                        IsAssigned = false,
                        RestaurantId = request.EntityId
                    };

                    var result = shiftService.LoadShifts(loadShiftRequest);

                    if (result.Shifts.Any())
                    {
                        foreach (var shift in result.Shifts)
                        {
                            var msg = string.Format("A shift scheduled for {0} has not been assigned.", shift.StartTime.ToString(SharedConstants.DateFormat));
                            notifications.AddMessage(new Notification(msg, NotificationSeverity.Information));
                        }
                    }
                }
                else
                {
                    notifications.AddError("No settings or notifications found.");
                }

                return new GenericServiceResponse
                {
                    NotificationCollection = notifications
                };
            });
        }
    }
}