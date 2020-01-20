using System.Collections.Generic;
using System;
using System.Linq;
using Framework.CustomTypes;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftTemplateViewModel : ShifterModelBase
    {
        #region Constructor

        public ShiftTemplateViewModel()
        {
            InitializeTemplate();
            TimeSlots = new List<ShiftTimeSlotDto>();
        }

        #endregion

        #region Properties

        public IEnumerable<ShiftTimeSlotDto> TimeSlots { get; set; }

        public List<ShiftTemplateDto> ShiftTemplates { get; set; }

        public int RestaurantId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the number of shifts for a specified day and time slot if it has been added to the template
        /// </summary>
        public int GetNumberOfShifts(DayOfWeekStartingAtMonday day, int? timeSlotId)
        {
            var template = ShiftTemplates.FirstOrDefault(st => st.DayOfWeek.ToString() == day.ToString() && st.TimeSlot.Id == timeSlotId);
            if (template.IsNotNull() && template.NumberOfWaitersNeeded > 0)
            {
                return template.NumberOfWaitersNeeded;
            }
            return 0;
        }

        private void InitializeTemplate()
        {
            if (ShiftTemplates.IsNull())
            {
                ShiftTemplates = new List<ShiftTemplateDto>();
            }
        }

        /// <summary>
        /// Returns the template id
        /// </summary>
        public int GetTemplateId(int templateIndex)
        {
            return ShiftTemplates.Count > templateIndex ? ShiftTemplates[templateIndex].Id : 0;
        }

        #endregion
    }
}