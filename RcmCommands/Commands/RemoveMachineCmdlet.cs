//---------------------------------------------------------------
//  <copyright file="RemoveMachineCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Remove-Machine cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Remove machine.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "Machine", DefaultParameterSetName = RcmParameterSets.Default)]
    public class RemoveMachineCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the machine identity.
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

            RcmClientForAgent.UnregisterMachine(
                this.MachineId,
                PSRcmClient.GetAgentOperationContext(this.MachineId));
            this.WriteObject("Machine unregistered from RCM.");
        }
    }
}
