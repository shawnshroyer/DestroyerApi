using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestroyerApi.Models
{
    /// <summary>
    ///   This is Account in which all things tie into
    /// </summary>
    public class Household
    {
        /// <summary>
        /// Id for the Household in question
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///   This is the name of the household
        /// </summary>
        public string Name { get; set; }
    }
}