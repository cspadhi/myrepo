﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NorthwindBackOffice.OrderServiceRef {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://dotnetmentors.com/services/order", ConfigurationName="OrderServiceRef.IOrderService")]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dotnetmentors.com/services/order/IOrderService/GetOrderDate", ReplyAction="http://dotnetmentors.com/services/order/IOrderService/GetOrderDateResponse")]
        string GetOrderDate(int orderID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dotnetmentors.com/services/order/IOrderService/GetOrderAmount", ReplyAction="http://dotnetmentors.com/services/order/IOrderService/GetOrderAmountResponse")]
        string GetOrderAmount(int orderID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://dotnetmentors.com/services/order/IOrderService/GetShipCountry", ReplyAction="http://dotnetmentors.com/services/order/IOrderService/GetShipCountryResponse")]
        string GetShipCountry(int orderID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : NorthwindBackOffice.OrderServiceRef.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.ClientBase<NorthwindBackOffice.OrderServiceRef.IOrderService>, NorthwindBackOffice.OrderServiceRef.IOrderService {
        
        public OrderServiceClient() {
        }
        
        public OrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetOrderDate(int orderID) {
            return base.Channel.GetOrderDate(orderID);
        }
        
        public string GetOrderAmount(int orderID) {
            return base.Channel.GetOrderAmount(orderID);
        }
        
        public string GetShipCountry(int orderID) {
            return base.Channel.GetShipCountry(orderID);
        }
    }
}
