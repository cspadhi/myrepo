//---------------------------------------------------------------
//  <copyright file="GetReplicationSettingsCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-ReplicationSettings cmdlet.
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
    /// Get replication settings.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ReplicationSettings", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(ReplicationSettings))]
    public class GetReplicationSettingsCmdlet : RcmCmdletBase
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

            ReplicationSettings replicationSettings = RcmClientForAgent.GetReplicationSettings(
                PSRcmClient.GetAgentOperationContext(this.MachineId));
            this.WriteObject(replicationSettings);
        }
    }
}
