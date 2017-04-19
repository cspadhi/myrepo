//---------------------------------------------------------------
//  <copyright file="StopTestFailoverToAzureCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Stop-TestFailoverToAzure cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Cancel test failover to Azure.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "TestFailoverToAzure", DefaultParameterSetName = RcmParameterSets.Default)]
    public class StopTestFailoverToAzureCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmClientForInterService.CancelTestFailoverToAzure(
                this.ProtectionPairId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Test failover to Azure cancelled.");
        }
    }
}
