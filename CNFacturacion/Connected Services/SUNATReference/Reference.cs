﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CNFacturacion.SUNATReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.sunat.gob.pe", ConfigurationName="SUNATReference.billService")]
    public interface billService {
        
        // CODEGEN: Parameter 'status' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:getStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="status")]
        CNFacturacion.SUNATReference.getStatusResponse getStatus(CNFacturacion.SUNATReference.getStatusRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:getStatus", ReplyAction="*")]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.getStatusResponse> getStatusAsync(CNFacturacion.SUNATReference.getStatusRequest request);
        
        // CODEGEN: Parameter 'applicationResponse' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendBill", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="applicationResponse")]
        CNFacturacion.SUNATReference.sendBillResponse sendBill(CNFacturacion.SUNATReference.sendBillRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendBill", ReplyAction="*")]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendBillResponse> sendBillAsync(CNFacturacion.SUNATReference.sendBillRequest request);
        
        // CODEGEN: Parameter 'ticket' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendPack", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ticket")]
        CNFacturacion.SUNATReference.sendPackResponse sendPack(CNFacturacion.SUNATReference.sendPackRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendPack", ReplyAction="*")]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendPackResponse> sendPackAsync(CNFacturacion.SUNATReference.sendPackRequest request);
        
        // CODEGEN: Parameter 'ticket' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendSummary", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="ticket")]
        CNFacturacion.SUNATReference.sendSummaryResponse sendSummary(CNFacturacion.SUNATReference.sendSummaryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendSummary", ReplyAction="*")]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendSummaryResponse> sendSummaryAsync(CNFacturacion.SUNATReference.sendSummaryRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.sunat.gob.pe")]
    public partial class statusResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] contentField;
        
        private string statusCodeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", Order=0)]
        public byte[] content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
                this.RaisePropertyChanged("content");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string statusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
                this.RaisePropertyChanged("statusCode");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStatus", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class getStatusRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public getStatusRequest() {
        }
        
        public getStatusRequest(string ticket) {
            this.ticket = ticket;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStatusResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class getStatusResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CNFacturacion.SUNATReference.statusResponse status;
        
        public getStatusResponse() {
        }
        
        public getStatusResponse(CNFacturacion.SUNATReference.statusResponse status) {
            this.status = status;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendBill", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendBillRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendBillRequest() {
        }
        
        public sendBillRequest(string fileName, byte[] contentFile, string partyType) {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendBillResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendBillResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] applicationResponse;
        
        public sendBillResponse() {
        }
        
        public sendBillResponse(byte[] applicationResponse) {
            this.applicationResponse = applicationResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendPack", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendPackRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendPackRequest() {
        }
        
        public sendPackRequest(string fileName, byte[] contentFile, string partyType) {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendPackResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendPackResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public sendPackResponse() {
        }
        
        public sendPackResponse(string ticket) {
            this.ticket = ticket;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendSummary", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendSummaryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendSummaryRequest() {
        }
        
        public sendSummaryRequest(string fileName, byte[] contentFile, string partyType) {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendSummaryResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendSummaryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public sendSummaryResponse() {
        }
        
        public sendSummaryResponse(string ticket) {
            this.ticket = ticket;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface billServiceChannel : CNFacturacion.SUNATReference.billService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class billServiceClient : System.ServiceModel.ClientBase<CNFacturacion.SUNATReference.billService>, CNFacturacion.SUNATReference.billService {
        
        public billServiceClient() {
        }
        
        public billServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public billServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public billServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public billServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CNFacturacion.SUNATReference.getStatusResponse CNFacturacion.SUNATReference.billService.getStatus(CNFacturacion.SUNATReference.getStatusRequest request) {
            return base.Channel.getStatus(request);
        }
        
        public CNFacturacion.SUNATReference.statusResponse getStatus(string ticket) {
            CNFacturacion.SUNATReference.getStatusRequest inValue = new CNFacturacion.SUNATReference.getStatusRequest();
            inValue.ticket = ticket;
            CNFacturacion.SUNATReference.getStatusResponse retVal = ((CNFacturacion.SUNATReference.billService)(this)).getStatus(inValue);
            return retVal.status;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.getStatusResponse> CNFacturacion.SUNATReference.billService.getStatusAsync(CNFacturacion.SUNATReference.getStatusRequest request) {
            return base.Channel.getStatusAsync(request);
        }
        
        public System.Threading.Tasks.Task<CNFacturacion.SUNATReference.getStatusResponse> getStatusAsync(string ticket) {
            CNFacturacion.SUNATReference.getStatusRequest inValue = new CNFacturacion.SUNATReference.getStatusRequest();
            inValue.ticket = ticket;
            return ((CNFacturacion.SUNATReference.billService)(this)).getStatusAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CNFacturacion.SUNATReference.sendBillResponse CNFacturacion.SUNATReference.billService.sendBill(CNFacturacion.SUNATReference.sendBillRequest request) {
            return base.Channel.sendBill(request);
        }
        
        public byte[] sendBill(string fileName, byte[] contentFile, string partyType) {
            CNFacturacion.SUNATReference.sendBillRequest inValue = new CNFacturacion.SUNATReference.sendBillRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            CNFacturacion.SUNATReference.sendBillResponse retVal = ((CNFacturacion.SUNATReference.billService)(this)).sendBill(inValue);
            return retVal.applicationResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendBillResponse> CNFacturacion.SUNATReference.billService.sendBillAsync(CNFacturacion.SUNATReference.sendBillRequest request) {
            return base.Channel.sendBillAsync(request);
        }
        
        public System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendBillResponse> sendBillAsync(string fileName, byte[] contentFile, string partyType) {
            CNFacturacion.SUNATReference.sendBillRequest inValue = new CNFacturacion.SUNATReference.sendBillRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((CNFacturacion.SUNATReference.billService)(this)).sendBillAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CNFacturacion.SUNATReference.sendPackResponse CNFacturacion.SUNATReference.billService.sendPack(CNFacturacion.SUNATReference.sendPackRequest request) {
            return base.Channel.sendPack(request);
        }
        
        public string sendPack(string fileName, byte[] contentFile, string partyType) {
            CNFacturacion.SUNATReference.sendPackRequest inValue = new CNFacturacion.SUNATReference.sendPackRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            CNFacturacion.SUNATReference.sendPackResponse retVal = ((CNFacturacion.SUNATReference.billService)(this)).sendPack(inValue);
            return retVal.ticket;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendPackResponse> CNFacturacion.SUNATReference.billService.sendPackAsync(CNFacturacion.SUNATReference.sendPackRequest request) {
            return base.Channel.sendPackAsync(request);
        }
        
        public System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendPackResponse> sendPackAsync(string fileName, byte[] contentFile, string partyType) {
            CNFacturacion.SUNATReference.sendPackRequest inValue = new CNFacturacion.SUNATReference.sendPackRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((CNFacturacion.SUNATReference.billService)(this)).sendPackAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CNFacturacion.SUNATReference.sendSummaryResponse CNFacturacion.SUNATReference.billService.sendSummary(CNFacturacion.SUNATReference.sendSummaryRequest request) {
            return base.Channel.sendSummary(request);
        }
        
        public string sendSummary(string fileName, byte[] contentFile, string partyType) {
            CNFacturacion.SUNATReference.sendSummaryRequest inValue = new CNFacturacion.SUNATReference.sendSummaryRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            CNFacturacion.SUNATReference.sendSummaryResponse retVal = ((CNFacturacion.SUNATReference.billService)(this)).sendSummary(inValue);
            return retVal.ticket;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendSummaryResponse> CNFacturacion.SUNATReference.billService.sendSummaryAsync(CNFacturacion.SUNATReference.sendSummaryRequest request) {
            return base.Channel.sendSummaryAsync(request);
        }
        
        public System.Threading.Tasks.Task<CNFacturacion.SUNATReference.sendSummaryResponse> sendSummaryAsync(string fileName, byte[] contentFile, string partyType) {
            CNFacturacion.SUNATReference.sendSummaryRequest inValue = new CNFacturacion.SUNATReference.sendSummaryRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((CNFacturacion.SUNATReference.billService)(this)).sendSummaryAsync(inValue);
        }
    }
}
