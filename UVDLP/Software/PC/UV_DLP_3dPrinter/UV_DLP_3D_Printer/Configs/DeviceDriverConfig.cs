﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Xml;
using UV_DLP_3D_Printer.Drivers;

namespace UV_DLP_3D_Printer.Configs
{
    public class DeviceDriverConfig
    {
        public eDriverType m_drivertype;
        public ConnectionConfig m_connection; // main serial connection to printer
        public ConnectionConfig m_displayconnection; // to the projector or similar
        public Boolean m_displayconnectionenabled;   // projector comm enabled / disabled -SHS

        public DeviceDriverConfig() 
        {
            m_drivertype = eDriverType.eGENERIC; // default to a null driver
            m_displayconnectionenabled = false;  // -SHS
            m_connection = new ConnectionConfig();
            m_displayconnection = new ConnectionConfig();
            m_connection.CreateDefault();
            m_displayconnection.CreateDefault();
            
        }

        public bool Load(XmlReader xr)
        {
            try
            {
                bool retval = false;
                xr.ReadStartElement("DriverConfig");
                    m_drivertype = (eDriverType)Enum.Parse(typeof(eDriverType), xr.ReadElementString("DriverType"));
                    m_displayconnectionenabled = Boolean.Parse(xr.ReadElementString("DisplayCommEnabled")); // -SHS
                    if (m_connection.Load(xr))
                    {
                        retval = true;
                    }
                    m_displayconnection.Load(xr);
                xr.ReadEndElement();
                return retval;
            }
            catch (Exception ex)
            {
                DebugLogger.Instance().LogRecord(ex.Message);
                return false;
            }
        }
        public bool Save(XmlWriter xw)
        {
            try
            {
                bool retval = false;
                xw.WriteStartElement("DriverConfig");
                    xw.WriteElementString("DriverType", m_drivertype.ToString());
                    xw.WriteElementString("DisplayCommEnabled", m_displayconnectionenabled.ToString()); // -SHS
                    if (m_connection.Save(xw))
                    {
                        retval = true;
                    }
                    m_displayconnection.Save(xw);
                xw.WriteEndElement();
                return retval;
            }
            catch (Exception ex)
            {
                DebugLogger.Instance().LogRecord(ex.Message);
                return false;
            }
        }        

    }
}
