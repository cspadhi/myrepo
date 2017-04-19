//---------------------------------------------------------------
//  <copyright file="RemoveGatewayServiceCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Remove-GatewayService cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Remove gateway service.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "GatewayService", DefaultParameterSetName = RcmParameterSets.Default)]
    public class RemoveGatewayServiceCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the identifier for the gateway service.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string GatewayId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmClientForInterService.UnregisterGatewayService(
                this.GatewayId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Gateway service got unregistered from RCM.");
        }
    }
}
