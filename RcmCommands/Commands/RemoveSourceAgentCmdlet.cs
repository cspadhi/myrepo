//---------------------------------------------------------------
//  <copyright file="RemoveSourceAgentCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Remove-SourceAgent cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Unregister source agent.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "SourceAgent", DefaultParameterSetName = RcmParameterSets.Default)]
    public class RemoveSourceAgentCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the source agent identity.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceAgentId { get; set; }

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

            RcmClientForAgent.UnregisterSourceAgent(
                this.SourceAgentId,
                PSRcmClient.GetAgentOperationContext(this.MachineId));
            this.WriteObject("Source agent unregistered from RCM.");
        }
    }
}
