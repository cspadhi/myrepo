//---------------------------------------------------------------
//  <copyright file="RcmParameterSets.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines parameter sets for RCM commands.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RcmCommands
{
    /// <summary>
    /// Parameter Sets used for RCM commands.
    /// </summary>
    internal static class RcmParameterSets
    {
        /// <summary>
        /// When nothing is passed to the command.
        /// </summary>
        internal const string Default = "Default";

        /// <summary>
        /// When force switch is passed to the command.
        /// </summary>
        internal const string Force = "Force";

        /// <summary>
        /// When object is passed to the command.
        /// </summary>
        internal const string ByObject = "ByObject";
    }
}