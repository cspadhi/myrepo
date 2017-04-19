//---------------------------------------------------------------
//  <copyright file="GetMachineCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-Machine cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Get machine details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Machine", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(MachineDetails))]
    public class GetMachineCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the identity of the machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string MachineId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            MachineDetails machineDetails = RcmClientForInterService.GetMachineDetails(
                this.MachineId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject(machineDetails);
        }
    }
}
