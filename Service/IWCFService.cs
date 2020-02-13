using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        [WebInvoke(Method = "OPTIONS",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "*")]
        void AcceptOptions();

        #region Devices
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllDevices")]
        List<Device> GetAllDevices();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetOneDevice?id={id}")]
        Device GetOneDevice(int id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "InsertDevice")]
        bool InsertDevice(string name, int manufacturer, int type, int OS, int processor, int RAM);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "UpdateDevice")]
        bool UpdateDevice(int id, string name, int manufacturer, int type, int OS, int processor, int RAM);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "DeleteDevice?id={id}")]
        bool DeleteDevice(int id);
        #endregion

        #region Manufacturers
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllManufacturers")]
        List<Manufacturer> GetAllManufacturers();
        #endregion

        #region DeviceTypes
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllDeviceTypes")]
        List<DeviceType> GetAllTypes();
        #endregion

        #region OperatingSystems
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllOS")]
        List<OS> GetAllOS();
        #endregion

        #region Processors
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllProcessors")]
        List<Processor> GetAllProcessors();
        #endregion

        #region Roles
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllRoles")]
        List<Role> GetAllRoles();
        #endregion

        #region Locations
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllLocations")]
        List<Location> GetAllLocations();
        #endregion

        #region Users
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Login?username={username}&password={password}")]
        User Login(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetOneUser?id={id}")]
        User GetOneUser(int id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "InsertUser")]
        bool InsertUser(string username, string password, string name, int role, int location);
        #endregion

        #region UsersDevices
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetUserForDevice?id={id}")]
        User GetUserForDevice(int id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Assign")]
        bool Assign(int device, int user);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Unassign")]
        bool Unassign(int device, int user);

        #endregion
    }
}
