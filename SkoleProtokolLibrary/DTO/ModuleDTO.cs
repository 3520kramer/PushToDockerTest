using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// Represents the data for a single module of a lesson.
    /// </summary>
    public class ModuleDTO
    {

        #region Properities

        /// <summary>
        /// The id of a module
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The time interval for a module
        /// </summary>
        public TimespanDTO Timespan { get; set; }

        #endregion

        #region Constructor

        public ModuleDTO()
        {
            
        }

        public ModuleDTO(DBModule module)
        {
            Id = module.Id;
            Timespan = new TimespanDTO(module.Timespan);
        }

        #endregion

    }
}
