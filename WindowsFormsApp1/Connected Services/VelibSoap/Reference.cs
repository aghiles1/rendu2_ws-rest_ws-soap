﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1.VelibSoap {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VelibSoap.IServiceVelib")]
    public interface IServiceVelib {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceVelib/getStations", ReplyAction="http://tempuri.org/IServiceVelib/getStationsResponse")]
        string[] getStations(string city);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceVelib/getStations", ReplyAction="http://tempuri.org/IServiceVelib/getStationsResponse")]
        System.Threading.Tasks.Task<string[]> getStationsAsync(string city);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceVelib/getAvailableBikes", ReplyAction="http://tempuri.org/IServiceVelib/getAvailableBikesResponse")]
        int getAvailableBikes(string city, string station, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceVelib/getAvailableBikes", ReplyAction="http://tempuri.org/IServiceVelib/getAvailableBikesResponse")]
        System.Threading.Tasks.Task<int> getAvailableBikesAsync(string city, string station, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceVelib/getTowns", ReplyAction="http://tempuri.org/IServiceVelib/getTownsResponse")]
        string[] getTowns();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceVelib/getTowns", ReplyAction="http://tempuri.org/IServiceVelib/getTownsResponse")]
        System.Threading.Tasks.Task<string[]> getTownsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceVelibChannel : WindowsFormsApp1.VelibSoap.IServiceVelib, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceVelibClient : System.ServiceModel.ClientBase<WindowsFormsApp1.VelibSoap.IServiceVelib>, WindowsFormsApp1.VelibSoap.IServiceVelib {
        
        public ServiceVelibClient() {
        }
        
        public ServiceVelibClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceVelibClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceVelibClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceVelibClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] getStations(string city) {
            return base.Channel.getStations(city);
        }
        
        public System.Threading.Tasks.Task<string[]> getStationsAsync(string city) {
            return base.Channel.getStationsAsync(city);
        }
        
        public int getAvailableBikes(string city, string station, int time) {
            return base.Channel.getAvailableBikes(city, station, time);
        }
        
        public System.Threading.Tasks.Task<int> getAvailableBikesAsync(string city, string station, int time) {
            return base.Channel.getAvailableBikesAsync(city, station, time);
        }
        
        public string[] getTowns() {
            return base.Channel.getTowns();
        }
        
        public System.Threading.Tasks.Task<string[]> getTownsAsync() {
            return base.Channel.getTownsAsync();
        }
    }
}