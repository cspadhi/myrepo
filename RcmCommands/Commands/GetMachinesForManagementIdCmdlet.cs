//---------------------------------------------------------------
//  <copyright file="GetMachinesForManagementIdCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-MachinesForManagementId cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Get machines for the management ID.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "MachinesForManagementId", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(IEnumerable<string>))]
    public class GetMachinesForManagementIdCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the management ID for the machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ManagementId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            List<string> machineIds = RcmClientForInterService.EnumerateMachinesForManagementId(
                this.ManagementId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject(machineIds);
        }
    }
}
