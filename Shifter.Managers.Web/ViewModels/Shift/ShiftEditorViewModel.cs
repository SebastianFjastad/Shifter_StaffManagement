using System;
using System.Collections.Generic;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class ShiftEditorViewModel : ShifterModelBase
    {
        #region Constructors

        public ShiftEditorViewModel()
        {
            Shifts = new List<ShiftDto>();
        }

        #endregion

        #region Properties

        public IEnumerable<ShiftDto> Shifts { get; set; }

        public IEnumerable<ShiftTimeSlotDto> TimeSlots { get; set; }

        public DateTime EditForDate { get; set; }

        public int WaiterId { get; set; }

        public int RestaurantId { get; set; }

        #endregion
    }

    public class EditedShift
    {
        public bool IsSaveSelected { get; set; }

        public bool IsEdit { get; set; }

        public ShiftDto Shift { get; set; }

        /// <summary>
        /// The shift already exists and is not marked for save
        /// </summary>
        public bool IsDelete
        {
            get
            {
                return !IsSaveSelected && !Shift.IsTransient();
            }
        }

        /// <summary>
        /// The shift already exists and is not marked for save
        /// </summary>
        public bool IsSave
        {
            get
            {
                return IsSaveSelected && (Shift.IsTransient() || IsEdit);
            }
        }
    }
}