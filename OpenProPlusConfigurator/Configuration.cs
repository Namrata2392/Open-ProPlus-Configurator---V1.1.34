using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Collections.ObjectModel;
using System.Data;
namespace OpenProPlusConfigurator
{
    public class PrivateTag
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string FC { get; set; }
        public string MappingType { get; set; }
        public string Rtype { get; set; }
        public string IedName { get; set; }
        public string Index { get; set; }
        public PrivateTag(int id, string mapping, string index, string reference, string fc, string desc, string iedname, string fetchType)
        {
            this.Id = id;
            this.Reference = reference;
            this.FC = fc;
            this.MappingType = mapping;
            this.Description = desc;
            this.IedName = iedname;
            this.Rtype = fetchType;
            this.Index = index;
        }
    }
    public interface IConfigure
    {
        Dictionary<string, string> Itypes
        {
            get;
        }
        Dictionary<string, XPathNodeIterator> Iieds
        {
            get;
        }
        Dictionary<string, PrivateTag> IadditionalTags
        {
            get;
        }
        ICollection<GridItem> IgridItems
        {
            get;
        }
        Dictionary<string, Dictionary<string, Bda>> IdaTypesDict
        {
            get;
        }
        Dictionary<string, LNType> ILNodeTypeDict
        {
            get;
        }
        Dictionary<string, Dictionary<string, Ditem>> IdoTypeDict
        {
            get;
        }
        Dictionary<string, LN> IlnDict
        {
            get;
        }
        Dictionary<string, List<Fcda>> IdataSets
        {
            get;
        }
        Dictionary<string, List<Fcda>> IdynamicDataSets
        {
            get;
        }
        Dictionary<string, List<Fcda>> ItempDataSets
        {
            get;
        }
        Dictionary<string, Report> Ireports
        {
            get;
        }
        ICollection<Report> ILocalRcb
        {
            get;
        }
        ICollection<Report> IUsedRcb
        {
            get;
        }
        ICollection<PrivateTag> IAnalogTags
        {
            get;
        }
        ICollection<PrivateTag> IParameterTags
        {
            get;
        }
        ICollection<PrivateTag> IIeddinTags
        {
            get;
        }
        ICollection<PrivateTag> IControlTags
        {
            get;
        }
        Dictionary<string, List<PrivateTag>> IIedMappings
        {
            get;
        }
        Dictionary<string, string> INotIncluded
        {
            get;
        }
        Dictionary<string, string> ISupportedType
        {
            get;
        }
        Dictionary<string, int> IColumnHeaders
        {
            get;
        }
        Dictionary<string, string> ISupportedFC
        {
            get;
        }
        string IOpenedFileName
        {
            get;
            set;
        }
        string IFullFileNamePath
        {
            get;
            set;
        }
    }
    public class Configure : XmlDocument, IConfigure
    {
        private int id;
        private int idDi = 0;
        private Boolean wrongEntry = false;
        private bool corruptedScl = false;
        public bool CorruptedScl
        {
            get
            {
                return corruptedScl;
            }
            set
            {
                corruptedScl = value;
            }
        }
        public Boolean WrongEntry
        {
            get
            {
                return wrongEntry;
            }
            set
            {
                wrongEntry = value;
            }
        }
        public Boolean MappingChanged
        {
            get;
            set;
        }
        private Dictionary<string, string> types = new Dictionary<string, string>();
        public Dictionary<string, string> Itypes
        {
            get
            {
                return types;
            }
        }
        private Dictionary<string, XPathNodeIterator> ieds;
        public Dictionary<string, XPathNodeIterator> Iieds
        {
            get
            {
                return ieds;
            }
        }
        private Dictionary<string, PrivateTag> additionalTags;
        public Dictionary<string, PrivateTag> IadditionalTags
        {
            get
            {
                return additionalTags;
            }
        }
        private List<GridItem> gridItems;
        public ICollection<GridItem> IgridItems
        {
            get
            {
                return gridItems as ICollection<GridItem>;
            }
        }
        private Dictionary<string, Dictionary<string, Bda>> daTypesDict;
        public Dictionary<string, Dictionary<string, Bda>> IdaTypesDict
        {
            get
            {
                return daTypesDict;
            }
        }
        private Dictionary<string, LNType> lNodeTypeDict;
        public Dictionary<string, LNType> ILNodeTypeDict
        {
            get
            {
                return lNodeTypeDict;
            }
        }
        private Dictionary<string, Dictionary<string, Ditem>> doTypeDict;
        public Dictionary<string, Dictionary<string, Ditem>> IdoTypeDict
        {
            get
            {
                return doTypeDict;
            }
        }
        private Dictionary<string, LN> lnDict;
        public Dictionary<string, LN> IlnDict
        {
            get
            {
                return lnDict;
            }
        }
        private Dictionary<string, List<Fcda>> dataSets = new Dictionary<string, List<Fcda>>();
        public Dictionary<string, List<Fcda>> IdataSets
        {
            get
            {
                return dataSets;
            }
        }
        private Dictionary<string, List<Fcda>> dynamicDataSets = new Dictionary<string, List<Fcda>>(); // collection containing dynamic Data Sets created by user 
        public Dictionary<string, List<Fcda>> IdynamicDataSets // Public access to dynamic data sets dictionary
        {
            get
            {
                return dynamicDataSets;
            }
        }
        private Dictionary<string, string> dynamicDataSetsInUse = new Dictionary<string, string>(); // collection containing dynamic Data Sets in use
        public Dictionary<string, string> IdynamicDataSetsInUse // Public access to dynamic data sets in use dictionary
        {
            get
            {
                return dynamicDataSetsInUse;
            }
        }
        private Dictionary<string, List<Fcda>> tempDataSets = new Dictionary<string, List<Fcda>>(); // collection containing temporary dynamic data sets
        public Dictionary<string, List<Fcda>> ItempDataSets // Public access to temporary data sets dictionary
        {
            get
            {
                return tempDataSets;
            }
        }
        private Dictionary<string, Report> reports = new Dictionary<string, Report>(); // dictionary containing reports
        public Dictionary<string, Report> Ireports // Public access to reports dictionary
        {
            get
            {
                return reports;
            }
        }
        private List<Report> localRcb = new List<Report>(); // list with rcb's and urcb's containing specified item
        public ICollection<Report> ILocalRcb // Public access to local RCB list
        {
            get
            {
                return localRcb as ICollection<Report>;
            }
        }
        private List<Report> usedRcb = new List<Report>();// list containg rcb and urcb that are used and will be included to SCL private section
        public ICollection<Report> IUsedRcb// Public access to used RCB list
        {
            get
            {
                return usedRcb as ICollection<Report>;
            }
        }
        private List<PrivateTag> analogTags = new List<PrivateTag>(); // Public access to analog tags List
        public ICollection<PrivateTag> IAnalogTags
        {
            get
            {
                return analogTags as ICollection<PrivateTag>;
            }
        }
        private List<PrivateTag> parameterTags = new List<PrivateTag>(); // Public access to parameter tags List
        public ICollection<PrivateTag> IParameterTags
        {
            get
            {
                return parameterTags as ICollection<PrivateTag>;
            }
        }
        private List<PrivateTag> ieddinTags = new List<PrivateTag>(); // Public access to IedDin tags List
        public ICollection<PrivateTag> IIeddinTags
        {
            get
            {
                return ieddinTags as ICollection<PrivateTag>;
            }
        }
        private List<PrivateTag> controlTags = new List<PrivateTag>(); // Public access to control tags List
        public ICollection<PrivateTag> IControlTags
        {
            get
            {
                return controlTags as ICollection<PrivateTag>;
            }
        }
        private Dictionary<string, List<PrivateTag>> iedMappings = new Dictionary<string, List<PrivateTag>>(); // Public access to Ied mapping dictionary
        public Dictionary<string, List<PrivateTag>> IIedMappings
        {
            get
            {
                return iedMappings;
            }
        }
        private Dictionary<string, string> notIncluded = new Dictionary<string, string> { // collection containig not included items 
            { "Test", "" },
            { "Check", "" },
            { "T", "" },
            { "q", "" },
            { "t", "" },
            { "ctlNum", "" },
            { "orCat", "" },
            { "orIdent", "" }
        };
        public Dictionary<string, string> INotIncluded // Public access to not included items collection
        {
            get
            {
                return notIncluded;
            }
        }
        private Dictionary<string, string> supportedType = new Dictionary<string, string> { // collection containig supported items type
            {"BOOLEAN",""},
            {"INT8",""},
            {"INT16",""},
            {"INT24",""},
            {"INT32",""},
            {"INT128",""},
            {"INT8U",""},
            {"INT16U",""},
            {"INT24U",""},
            {"INT32U",""},
            {"FLOAT32",""},
            {"FLOAT64",""},
            {"Enum",""},
            {"Dbpos",""}
        };
        public Dictionary<string, string> ISupportedType // Public access to collection of  supported types
        {
            get
            {
                return supportedType;
            }
        }
        private Dictionary<string, int> columnHeaders = new Dictionary<string, int> {
            {"Id",0},
            {"Mapping Type",1},
            {"Index",2},
            {"Refresh Type",3},
            {"Type",4},
            {"Object Reference",5},
            {"FC",6},
            {"Description",7}
        };
        public Dictionary<string, int> IColumnHeaders // Public access to grid column headers collection
        {
            get
            {
                return columnHeaders;
            }
        }
        private Dictionary<string, string> supportedFC = new Dictionary<string, string>(); // collection containig supported FC
        public Dictionary<string, string> ISupportedFC // Public access to supported functional constrains collection
        {
            get
            {
                return supportedFC;
            }
        }
        private string fileName = "";
        public string IOpenedFileName // Public access to opened file name
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }
        private string fullFileNamePath = "";
        public string IFullFileNamePath // Public access to full path of opened SCL file
        {
            get
            {
                return fullFileNamePath;
            }
            set
            {
                fullFileNamePath = value;
            }
        }
        private Boolean skipAllWrongItems;
        public Configure() // constructor thats initialize all collections 
        {
            skipAllWrongItems = false;
            MappingChanged = false;
            types = new Dictionary<string, string>();
            ieds = new Dictionary<string, XPathNodeIterator>();
            additionalTags = new Dictionary<string, PrivateTag>();
            gridItems = new List<GridItem>();
            lnDict = new Dictionary<string, LN>();
            daTypesDict = new Dictionary<string, Dictionary<string, Bda>>();
            lNodeTypeDict = new Dictionary<string, LNType>();
            doTypeDict = new Dictionary<string, Dictionary<string, Ditem>>();
            dataSets = new Dictionary<string, List<Fcda>>();
            reports = new Dictionary<string, Report>();
            id = 0;
            CorruptedScl = false;
        }
        
        DataRow DrOnReq;
        private void AddRowToGrid(int idDi, bool ischecked, string type, string reference, string FC, string desc, string ln, string iedName, string model)
        {
            try
            {
                gridItems.Add(new GridItem(id, idDi, ischecked, type, reference, FC, desc, ln, iedName, model));
                DrOnReq = Utils.OnReqData.NewRow();
                DrOnReq["Onrequest"] = reference;
                DrOnReq["Node"] = "On Request";
                DrOnReq["FC"] = FC.ToString();
                Utils.OnReqData.Rows.Add(DrOnReq);
            }
            catch (System.ArgumentException)
            {
                throw new System.ArgumentException("Duplicated Grid Item entry: " + reference);
            }
            id++;
            
        }
        public void ChooseupdatedItems() // Method thats choose items to gridview - traverse LN, DO, DA
        {
            foreach (var ln in IlnDict)
            {
                string iedName = Utils.UpdatedIEC61850ServerICDName;
                //string iedName = ln.Value.IedName;
                LNType lnt;
                if (!ILNodeTypeDict.ContainsKey(ln.Value.LNType))
                {
                    continue;
                }
                lnt = ILNodeTypeDict[ln.Value.LNType];
                foreach (var doLNodeType in lnt.DataObjects)
                {
                    string doType = doLNodeType.Value;
                    Dictionary<string, Ditem> daDict;

                    if (!IdoTypeDict.ContainsKey(doType))
                    {
                        continue;
                    }
                    daDict = IdoTypeDict[doType];
                    foreach (var da in daDict)
                    {
                        if (da.Value.BType == "Quality" || da.Value.BType == "Timestamp")
                        {
                            continue;
                        }
                        if (da.Value.Sdo)
                        {
                            string sdoName = da.Value.Name;
                            string sdoType = da.Value.DIType;
                            string model = "";
                            foreach (var t in daDict)
                            {
                                if (t.Key.Contains("ctlModel"))
                                {
                                    model = t.Value.ctlModel;
                                    break;
                                }
                            }

                            Explore(true, sdoType, ref sdoName, iedName +
                                                                ln.Value.LDeviceName + "/" +
                                                                ln.Value.Prefix +
                                                                ln.Value.LNClass +
                                                                ln.Value.LNInstance + "." +
                                                                doLNodeType.Key + ".", da.Value.FC, ln.Value.LNType, ln.Value.IedName, da.Value.Count, model);
                        }
                        else
                        {
                            Ditem tmp = da.Value;
                            string model = "";
                            foreach (var t in daDict)
                            {
                                if (t.Key.Contains("ctlModel"))
                                {
                                    model = t.Value.ctlModel;
                                    break;
                                }
                            }
                            if (tmp.BType == "Struct")
                            {
                                string tmpDaType = tmp.DIType;
                                string nameTmp = tmp.Name;

                                Explore(false, tmpDaType, ref nameTmp, iedName +
                                                                        ln.Value.LDeviceName + "/" +
                                                                        ln.Value.Prefix +
                                                                        ln.Value.LNClass +
                                                                        ln.Value.LNInstance + "." +
                                                                        doLNodeType.Key + ".", da.Value.FC, ln.Value.LNType, ln.Value.IedName, da.Value.Count,
                                                                        model);
                            }
                            else
                            {
                                if (supportedType.ContainsKey(da.Value.BType))
                                {
                                    if (string.IsNullOrEmpty(da.Value.Count))
                                    {
                                        if (model != "")
                                        {
                                            AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                            ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                            ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                            da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                        }
                                        else
                                        {
                                            AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName + ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                                da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, da.Value.ctlModel);
                                        }
                                    }
                                    else
                                    {
                                        Int32 i = Convert.ToInt32(da.Value.Count, CultureInfo.CurrentCulture);
                                        if (i == 0)
                                        {
                                            if (model != "")
                                            {
                                                AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                                ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                                da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                            }
                                            else
                                            {
                                                AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName + ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                    ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                                    da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, da.Value.ctlModel);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < i; j++)
                                            {
                                                if (model != "")
                                                {
                                                    AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                                        ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                        ln.Value.LNInstance + "." + doLNodeType.Key + "." + da.Value.Name + "[" +
                                                                                                        j.ToString(CultureInfo.InvariantCulture) + "]", da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                                }
                                                else
                                                {
                                                    AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                                        ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                        ln.Value.LNInstance + "." + doLNodeType.Key + "." + da.Value.Name + "[" +
                                                                                                        j.ToString(CultureInfo.InvariantCulture) + "]", da.Value.FC, "", ln.Key, ln.Value.IedName,
                                                                                                        da.Value.ctlModel);
                                                }
                                            }
                                        }
                                    }
                                    if (!types.ContainsKey(da.Value.BType))
                                    {
                                        types.Add(da.Value.BType, da.Value.BType);
                                    }
                                    if (!ISupportedFC.ContainsKey(da.Value.FC))
                                    {
                                        ISupportedFC.Add(da.Value.FC, "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ChooseItems() // Method thats choose items to gridview - traverse LN, DO, DA
        {
            Utils.OnReqData.Rows.Clear(); Utils.OnReqData.Columns.Clear();
            DataColumn dccolumn1 = Utils.OnReqData.Columns.Add("OnRequest", typeof(string));
            Utils.OnReqData.Columns.Add("Node", typeof(string));
            Utils.OnReqData.Columns.Add("FC", typeof(string));
            foreach (var ln in IlnDict)
            {
                string iedName = ln.Value.IedName;
                LNType lnt;
                if (!ILNodeTypeDict.ContainsKey(ln.Value.LNType))
                {
                    continue;
                }
                lnt = ILNodeTypeDict[ln.Value.LNType];
                foreach (var doLNodeType in lnt.DataObjects)
                {
                    string doType = doLNodeType.Value;
                    Dictionary<string, Ditem> daDict;

                    if (!IdoTypeDict.ContainsKey(doType))
                    {
                        continue;
                    }
                    daDict = IdoTypeDict[doType];
                    foreach (var da in daDict)
                    {
                        if (da.Value.BType == "Quality" || da.Value.BType == "Timestamp")
                        {
                            continue;
                        }
                        if (da.Value.Sdo)
                        {
                            string sdoName = da.Value.Name;
                            string sdoType = da.Value.DIType;
                            string model = "";
                            foreach (var t in daDict)
                            {
                                if (t.Key.Contains("ctlModel"))
                                {
                                    model = t.Value.ctlModel;
                                    break;
                                }
                            }

                            Explore(true, sdoType, ref sdoName, iedName +
                                                                ln.Value.LDeviceName + "/" +
                                                                ln.Value.Prefix +
                                                                ln.Value.LNClass +
                                                                ln.Value.LNInstance + "." +
                                                                doLNodeType.Key + ".", da.Value.FC, ln.Value.LNType, ln.Value.IedName, da.Value.Count, model);
                        }
                        else
                        {
                            Ditem tmp = da.Value;
                            string model = "";
                            foreach (var t in daDict)
                            {
                                if (t.Key.Contains("ctlModel"))
                                {
                                    model = t.Value.ctlModel;
                                    break;
                                }
                            }
                            if (tmp.BType == "Struct")
                            {
                                string tmpDaType = tmp.DIType;
                                string nameTmp = tmp.Name;

                                Explore(false, tmpDaType, ref nameTmp, iedName +
                                                                        ln.Value.LDeviceName + "/" +
                                                                        ln.Value.Prefix +
                                                                        ln.Value.LNClass +
                                                                        ln.Value.LNInstance + "." +
                                                                        doLNodeType.Key + ".", da.Value.FC, ln.Value.LNType, ln.Value.IedName, da.Value.Count,
                                                                        model);
                            }
                            else
                            {
                                if (supportedType.ContainsKey(da.Value.BType))
                                {
                                    if (string.IsNullOrEmpty(da.Value.Count))
                                    {
                                        if (model != "")
                                        {
                                            AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                            ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                            ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                            da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                        }
                                        else
                                        {
                                            AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName + ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                                da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, da.Value.ctlModel);
                                        }
                                    }
                                    else
                                    {
                                        Int32 i = Convert.ToInt32(da.Value.Count, CultureInfo.CurrentCulture);
                                        if (i == 0)
                                        {
                                            if (model != "")
                                            {
                                                AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                                ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                                da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                            }
                                            else
                                            {
                                                AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName + ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                    ln.Value.LNInstance + "." + doLNodeType.Key + "." +
                                                                                                    da.Value.Name, da.Value.FC, "", ln.Key, ln.Value.IedName, da.Value.ctlModel);
                                            }
                                        }
                                        else
                                        {
                                            for (int j = 0; j < i; j++)
                                            {
                                                if (model != "")
                                                {
                                                    AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                                        ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                        ln.Value.LNInstance + "." + doLNodeType.Key + "." + da.Value.Name + "[" +
                                                                                                        j.ToString(CultureInfo.InvariantCulture) + "]", da.Value.FC, "", ln.Key, ln.Value.IedName, model);
                                                }
                                                else
                                                {
                                                    AddRowToGrid(da.Value.Id, false, da.Value.BType, iedName +
                                                                                                        ln.Value.LDeviceName + "/" + ln.Value.Prefix + ln.Value.LNClass +
                                                                                                        ln.Value.LNInstance + "." + doLNodeType.Key + "." + da.Value.Name + "[" +
                                                                                                        j.ToString(CultureInfo.InvariantCulture) + "]", da.Value.FC, "", ln.Key, ln.Value.IedName,
                                                                                                        da.Value.ctlModel);
                                                }
                                            }
                                        }
                                    }
                                    if (!types.ContainsKey(da.Value.BType))
                                    {
                                        types.Add(da.Value.BType, da.Value.BType);
                                    }
                                    if (!ISupportedFC.ContainsKey(da.Value.FC))
                                    {
                                        ISupportedFC.Add(da.Value.FC, "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void Explore(bool isSdo, string type, ref string name, string prefix, string FC, string lnName, string IedName, string count, string model)
        {
            if (isSdo)
            {
                if (IdoTypeDict.ContainsKey(type))
                {
                    Dictionary<string, Ditem> daDictTmp = IdoTypeDict[type];
                    foreach (var v in daDictTmp)
                    {
                        if (supportedType.ContainsKey(v.Value.BType))
                        {
                            continue;
                        }
                        if (v.Value.Sdo)
                        {
                            string tempName = name;
                            name = name + "." + v.Value.Name;
                            type = v.Value.DIType;
                            model = v.Value.ctlModel;
                            Explore(true, type, ref name, prefix, v.Value.FC, lnName, IedName, count, model);
                            name = tempName;
                        }
                        else
                        {
                            Ditem tmp = v.Value;

                            if (tmp.BType == "Struct")
                            {
                                string tempName = name;
                                type = tmp.DIType;
                                name = name + "." + tmp.Name;
                                model = tmp.ctlModel;
                                Explore(false, type, ref name, prefix, tmp.FC, lnName, IedName, count, model);
                                name = tempName;
                            }
                            else
                            {
                                if (supportedType.ContainsKey(tmp.BType) && !notIncluded.ContainsKey(tmp.Name))
                                {
                                    if (string.IsNullOrEmpty(count))
                                    {
                                        model = v.Value.ctlModel;
                                        AddRowToGrid(v.Value.Id, false, tmp.BType, prefix + name + "." + tmp.Name, v.Value.FC, "", lnName, IedName, model);
                                    }
                                    else
                                    {
                                        Int32 i = Convert.ToInt32(count, CultureInfo.CurrentCulture);
                                        for (int j = 0; j < i; j++)
                                        {
                                            string[] split = name.Split('.');
                                            string tmp2 = split[0] + ".[" + j.ToString(CultureInfo.InvariantCulture) + "]";

                                            for (int z = 1; z < split.Length; z++)
                                            {
                                                tmp2 += ".";
                                                tmp2 += split[z];
                                            }
                                            model = v.Value.ctlModel;
                                            AddRowToGrid(v.Value.Id, false, tmp.BType, prefix + tmp2 + "." + tmp.Name, v.Value.FC, "", lnName, IedName, model);
                                        }
                                    }
                                    if (!types.ContainsKey(tmp.BType))
                                    {
                                        types.Add(tmp.BType, tmp.BType);
                                    }
                                    if (!ISupportedFC.ContainsKey(tmp.FC))
                                    {
                                        ISupportedFC.Add(tmp.FC, "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (IdaTypesDict.ContainsKey(type))
                {
                    var tmp = IdaTypesDict[type];
                    foreach (var v in tmp.Values)
                    {
                        if (v.BType == "Struct")
                        {
                            string tempName = name;
                            type = v.BdaType;
                            name = name + "." + v.Name;
                            Explore(false, type, ref name, prefix, FC, lnName, IedName, count, model);
                            name = tempName;
                        }
                        else
                        {
                            if (supportedType.ContainsKey(v.BType) && !notIncluded.ContainsKey(v.Name))
                            {
                                if (string.IsNullOrEmpty(count))
                                {
                                    AddRowToGrid(v.Id, false, v.BType, prefix + name + "." + v.Name, FC, "", lnName, IedName, model);
                                }
                                else
                                {
                                    Int32 i = Convert.ToInt32(count, CultureInfo.CurrentCulture);
                                    for (int j = 0; j < i; j++)
                                    {
                                        string[] split = name.Split('.');
                                        string tmp2 = split[0] + ".[" + j.ToString(CultureInfo.InvariantCulture) + "]";

                                        for (int z = 1; z < split.Length; z++)
                                        {
                                            tmp2 += ".";
                                            tmp2 += split[z];
                                        }
                                        AddRowToGrid(v.Id, false, v.BType, prefix + tmp2 + "." + v.Name, FC, "", lnName, IedName, model);
                                    }
                                }

                                if (!types.ContainsKey(v.BType))
                                {
                                    types.Add(v.BType, v.BType);
                                }
                                if (!ISupportedFC.ContainsKey(FC))
                                {
                                    ISupportedFC.Add(FC, "");
                                }
                            }
                        }
                    }
                }
            }
            return;
        }
        string Manufacture = "";
        IEC61850ServerSlaveGroup iec = new IEC61850ServerSlaveGroup();
        ucGroup61850ServerSlave ucmbs = new ucGroup61850ServerSlave();
        public void ParseScl()
        {
            string limitsIedName = "";
            int cSize = 0;
            int iSize = 0;
            int aSize = 0;
            int pSize = 0;
            ParseDATypes();
            ParserRemoteIP();
            ParserIEDName();
            TmpParseLnodeType();
            TmpParseDOtype();
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator iedIterator = navigator.SelectDescendants("IED", navigator.NamespaceURI, true);
            XPathNodeIterator privateLimitsIterator = navigator.SelectDescendants("Limits", "http://www.ashidaelectronics.com/", false);

            string ieDName = "";
            while (iedIterator.MoveNext())
            {
                if (iedIterator.Current != null)
                {
                    XPathNodeIterator tmp = iedIterator.Clone();
                    XPathNodeIterator tmp2 = iedIterator.Clone();
                    //Namrata: 24/10/2017
                    XPathNodeIterator tmp12 = iedIterator.Clone();
                    if (tmp.Current.Name == "IED")
                    {
                        tmp.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp.Current.Name == "name")
                            {
                                if (tmp.Current.Value == "")
                                {
                                    MessageBox.Show("IED node name value is empty.\nPlease check SCL file", "SCL parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    CorruptedScl = true;
                                    break;
                                }
                                ieDName = tmp.Current.Value;
                                try
                                {
                                    Iieds.Add(ieDName, tmp2);
                                    IIedMappings.Add(ieDName + ".Analog", new List<PrivateTag>());
                                    IIedMappings.Add(ieDName + ".Parameter", new List<PrivateTag>());
                                    IIedMappings.Add(ieDName + ".IedDin", new List<PrivateTag>());
                                    IIedMappings.Add(ieDName + ".Control", new List<PrivateTag>());
                                }
                                catch (System.ArgumentException)
                                {
                                    throw new System.ArgumentException("Duplicated Ied entry: " + ieDName);
                                }
                                tmp.Current.MoveToParent();
                            }
                        } while (tmp.Current.MoveToNextAttribute());
                    }
                }
            }
            while (privateLimitsIterator.MoveNext())
            {
                XPathNodeIterator temp = privateLimitsIterator.Clone();
                temp.Current.MoveToFirstAttribute();
                cSize = 0;
                iSize = 0;
                aSize = 0;
                pSize = 0;
                do
                {
                    if (temp.Current.Name == "iedName")
                    {
                        limitsIedName = temp.Current.Value;

                    }
                    if (temp.Current.Name == "controls")
                    {
                        cSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }
                    if (temp.Current.Name == "analogs")
                    {
                        aSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }
                    if (temp.Current.Name == "iedDins")
                    {
                        iSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }
                    if (temp.Current.Name == "parameters")
                    {
                        pSize = Convert.ToInt32(temp.Current.Value, CultureInfo.InvariantCulture);
                    }

                } while (temp.Current.MoveToNextAttribute());
                List<PrivateTag> analogTags = new List<PrivateTag>(new PrivateTag[aSize]);
                List<PrivateTag> parameterTags = new List<PrivateTag>(new PrivateTag[pSize]);
                List<PrivateTag> ieddinTags = new List<PrivateTag>(new PrivateTag[iSize]);
                List<PrivateTag> controlTags = new List<PrivateTag>(new PrivateTag[cSize]);
                IIedMappings[limitsIedName + ".Analog"] = analogTags;
                IIedMappings[limitsIedName + ".Parameter"] = parameterTags;
                IIedMappings[limitsIedName + ".IedDin"] = ieddinTags;
                IIedMappings[limitsIedName + ".Control"] = controlTags;
            }
            iedIterator = navigator.SelectDescendants("IED", navigator.NamespaceURI, true);
            while (iedIterator.MoveNext())
            {
                if (iedIterator.Current != null)
                {
                    XPathNodeIterator tmp = iedIterator.Clone();
                    if (tmp.Current.Name == "IED")
                    {
                        tmp.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp.Current.Name == "name")
                            {
                                ieDName = tmp.Current.Value;
                                Utils.IEDName = ieDName;
                                //tmp.Current.MoveToParent();
                            }
                            //Namrata: 24/10/2017
                            if (tmp.Current.Name == "manufacturer")
                            {
                                Manufacture = tmp.Current.Value;
                                //Namrata: 02/11/2017
                                Utils.Manufacture = Manufacture;
                                tmp.Current.MoveToParent();
                            }
                        } while (tmp.Current.MoveToNextAttribute());
                    }
                    XPathNodeIterator LdeviceIterator = tmp.Current.SelectDescendants("LDevice", navigator.NamespaceURI, true);
                    while (LdeviceIterator.MoveNext())
                    {
                        XPathNodeIterator lDeviceTmp = LdeviceIterator.Clone();
                        string logicalDeviceName = "";
                        if (lDeviceTmp.Current != null)
                        {
                            if (lDeviceTmp.Current.Name == "LDevice")
                            {
                                lDeviceTmp.Current.MoveToFirstAttribute();
                                do
                                {
                                    if (lDeviceTmp.Current.Name == "inst")
                                    {
                                        logicalDeviceName = lDeviceTmp.Current.Value;
                                    }
                                } while (lDeviceTmp.Current.MoveToNextAttribute());

                                if (!string.IsNullOrEmpty(logicalDeviceName))
                                {
                                    lDeviceTmp.Current.MoveToParent();
                                }
                                else
                                {
                                    MessageBox.Show("Logical device node name value is empty.\n Please check SCL file", "SCL parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    CorruptedScl = true;
                                    break;
                                }
                            }
                        }
                        ParseLn(logicalDeviceName, ieDName, LdeviceIterator);
                        //Namrata:24/10/2017
                        Utils.LogicalDeviceName = logicalDeviceName;

                    }
                }
            }
            if (CorruptedScl == true)
            {
                //clear containers and break parsing
                Iieds.Clear();
                IIedMappings.Clear();
                lnDict.Clear();
                return;
            }
            ChooseItems();
            ////Namrata: 2/11/2017
            //ChooseupdatedItems();
            ParsePrivateSection();
        }

        /// <summary>
        /// Method builds dictionary with existing Private tags from eSCL, allows reediting functionality
        /// </summary>
        /// <returns></returns>
        private void ParsePrivateSection()
        {
            string address = "";
            string dataset = "";
            string iedName = "";
            string trgOpt = "";
            string iPrd = "";
            string confRev = "";
            string bufTime = "";
            string custom = "";
            string fc = "";
            string desc = "";
            string type = "";
            string rType = "";
            string index = "";
            int lastCOid = -1;
            int tmpIndex = 0;
            int iter = 0;
            string findRep = "";
            Boolean skipRCBItem = false;
            Boolean skipDIItem = false;



            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator privateRCBIterator = navigator.SelectDescendants("RCB", "http://www.ashidaelectronics.com/", false);

            while (privateRCBIterator.MoveNext())
            {
                XPathNodeIterator tmp = privateRCBIterator.Clone();
                tmp.Current.MoveToFirstAttribute();
                address = "";
                dataset = "";
                iedName = "";
                trgOpt = "0";
                iPrd = "0";
                confRev = "";
                bufTime = "0";
                custom = "";


                do
                {
                    if (tmp.Current.Name == "address")
                    {
                        address = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "iedName")
                    {
                        iedName = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "dataset")
                    {
                        dataset = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "triggerOptions")
                    {
                        trgOpt = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "integrityPeriod")
                    {
                        iPrd = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "configRevision")
                    {
                        confRev = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "bufferTime")
                    {
                        bufTime = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "custom")
                    {
                        custom = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                skipRCBItem = VerifyRCBItem(privateRCBIterator, address, iedName, dataset, trgOpt, confRev, custom);


                if (skipRCBItem != true)
                {
                    string[] temp = address.Split('.', '/');
                    string[] dsNameTmp = dataset.Split('.', '/');
                    string ld = temp[0].Substring(iedName.Length);

                    string repFromAddres = FindReportFromAddress(address);
                    Report report = Ireports[repFromAddres];

                    report.Address = address;
                    report.IedName = iedName;
                    report.LdeviceName = ld;
                    report.LNName = temp[1];
                    report.ReportName = temp[2];

                    if (dsNameTmp.Length > 1)
                    {
                        report.DSName = dsNameTmp[2];
                    }
                    else
                    {
                        report.DSName = dsNameTmp[0];
                    }

                    if (report.DSName.StartsWith("@", StringComparison.CurrentCulture))
                    {
                        report.DSAddressDots = report.DSName + "." + iedName + "." + ld + "." + report.LNName;
                        foreach (var key in dynamicDataSets.Keys)
                        {
                            if (key.Contains(report.DSName + ".") && key.Contains(report.IedName))
                            {
                                report.DSAddressDots = key;
                            }
                        }
                        report.DSAddress = report.DSName;
                    }
                    else
                    {
                        report.DSAddress = iedName + ld + "/" + temp[1] + "." + report.DSName;
                        report.DSAddressDots = report.DSName + "." + iedName + "." + ld + "." + report.LNName;
                    }


                    if (dsNameTmp.Length == 1)
                    {
                        reports[repFromAddres].DSName = dsNameTmp[0];
                        if (dynamicDataSets != null)
                        {
                            if (dynamicDataSets.Count > 0)
                            {
                                bool toAdd = false;
                                foreach (var key in dynamicDataSets.Keys)
                                {
                                    if (!key.Contains(report.DSName) /*&& !key.Contains(report.IedName)*/)
                                    {
                                        toAdd = true;
                                    }
                                }
                                if (toAdd)
                                {
                                    dynamicDataSets.Add(report.DSAddressDots, null);
                                }
                            }
                            else
                            {
                                dynamicDataSets.Add(report.DSAddressDots, null);
                            }
                        }
                    }
                    else
                    {
                        if (dynamicDataSets != null)
                        {
                            if (!dataSets.ContainsKey(report.DSAddressDots) && (!dynamicDataSets.ContainsKey(report.DSAddressDots)))
                            {
                                dynamicDataSets.Add(report.DSAddressDots, null);
                            }
                        }
                    }
                    report.IntgPD = iPrd;
                    report.RptId = address.Replace('.', '$');
                    report.ConfRev = confRev;
                    report.BufTime = bufTime;
                    report.TrgOptNum = trgOpt;
                    report.Custom = custom;

                    if (!usedRcb.Contains(report))
                    {
                        usedRcb.Add(report);
                    }
                    skipRCBItem = false;
                }
            }

            XPathNodeIterator privateDiIterator = navigator.SelectDescendants("DI", "http://www.ashidaelectronics.com/", false);

            while (privateDiIterator.MoveNext())
            {
                XPathNodeIterator tmp = privateDiIterator.Clone();
                tmp.Current.MoveToFirstAttribute();
                address = "";
                fc = "";
                iedName = "";
                desc = "";
                type = "";
                rType = "";
                index = "";


                do
                {
                    if (tmp.Current.Name == "address")
                    {
                        address = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "fc")
                    {
                        fc = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "iedName")
                    {
                        iedName = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "mType")
                    {
                        type = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "description")
                    {
                        desc = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "rType")
                    {
                        rType = tmp.Current.Value;
                    }
                    if (tmp.Current.Name == "index")
                    {
                        index = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                skipDIItem = VerifyDataItem(privateDiIterator, address, fc, iedName, type, rType, index);
                findRep = FindReportFromAddress(rType);

                if (skipDIItem != true && (rType.Equals("On Request") || usedRcb.Contains(reports[findRep])))
                {
                    bool foundInGridItems = false;
                    foreach (GridItem item in IgridItems)
                    {
                        if ((item.IdDI != lastCOid
                            && item.ObjectReference == address
                            && item.FC == fc)
                            || item.IdDI == lastCOid
                            && item.ObjectReference == address
                            && (!item.FC.Equals("CO")))
                        {
                            item.IsChecked = true;
                            item.MappingType = type;
                            item.Description = desc;
                            item.RefreshType = rType;
                            item.Index = index;
                            lastCOid = item.IdDI;
                            foundInGridItems = true;
                            break;
                        }
                    }
                    if (foundInGridItems == false)
                    {

                        if (skipAllWrongItems == true)
                        {
                            MappingChanged = true;
                        }
                        else
                        {
                            DialogResult drs = MessageBox.Show("Invalid Data Item entry:\n\n" + privateDiIterator.Current.OuterXml.ToString() + "\n\nDo you want to skip this entry?"
                                                , "Private section parsing error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                            if (drs == DialogResult.Yes)
                            {
                                DialogResult dialRes = MessageBox.Show("Do you want to skip ALL invalid entries?"
                                                 , "Private section parsing error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                                     MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                                if (dialRes == DialogResult.Yes)
                                {
                                    skipAllWrongItems = true;
                                }
                                else
                                {
                                    skipAllWrongItems = false;
                                }
                                MappingChanged = true;
                            }
                            if (drs == DialogResult.No)
                            {

                                skipAllWrongItems = false;
                                wrongEntry = true;
                                dynamicDataSets.Clear();
                                dynamicDataSetsInUse.Clear();
                                usedRcb.Clear();
                                additionalTags.Clear();
                                iedMappings.Clear();
                                reports.Clear();
                                gridItems.Clear();
                                controlTags.Clear();
                                ieddinTags.Clear();
                                parameterTags.Clear();
                                analogTags.Clear();
                                localRcb.Clear();
                                dataSets.Clear();
                                lnDict.Clear();
                                lNodeTypeDict.Clear();
                                doTypeDict.Clear();
                                daTypesDict.Clear();

                                throw new System.ArgumentException("Data Item entry has at least one invalid or missing attribute.");
                            }
                        }

                        continue;
                    }
                    tmpIndex = Convert.ToInt32(index, CultureInfo.InvariantCulture);

                    if (lastCOid != -1
                            && !additionalTags.ContainsKey(address + fc + lastCOid))
                    {
                        additionalTags.Add(address + fc + lastCOid, new PrivateTag(lastCOid, type, index, address, fc, desc, iedName, rType));

                        if (tmpIndex > IIedMappings[iedName + "." + type].Count)
                        {
                            iedMappings[iedName + "." + type].Add(additionalTags[address + fc + lastCOid]);
                        }
                        else
                        {
                            iedMappings[iedName + "." + type][tmpIndex - 1] = additionalTags[address + fc + lastCOid];
                        }

                    }
                }
                skipDIItem = false;

            }
            foreach (List<PrivateTag> ied in iedMappings.Values)
            {
                iter = ied.Count;
                while ((iter > 0) && (ied[iter - 1] == null))
                {
                    ied.RemoveAt(iter - 1);
                    iter--;
                }
            }
        }


        /// <summary>
        /// Method verifies that selected Report is used in configuration - check that there is DI assigned to this Report 
        /// </summary>
        /// <param name="rpt">Report that should be checked</param>
        /// <param name="tags">Mapped Data Item dictinary</param>
        /// <returns></returns>
        private Boolean isRCBused(Report rpt, Dictionary<string, PrivateTag> tags)
        {

            foreach (var ptag in tags.Values)
            {

                if (ptag.Rtype.Equals(rpt.Address))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Method that is used for validation RCB entry. Validation contains attributes and syntax.
        /// </summary>
        /// <param name="privateRCBIterator">XML Iterator of selected RCB entry</param>
        /// <param name="address">Address attribute that is assigned to selected RCB entry</param>
        /// <param name="iedName">iedName attribute that is assigned to selected RCB entry</param>
        /// <param name="dataset">dataset attribute that is assigned to selected RCB entry</param>
        /// <param name="trgOpt">trgOpt attribute that is assigned to selected RCB entry</param>
        /// <param name="confRev">confRev attribute that is assigned to selected RCB entry</param>
        /// <param name="custom"> custom attribute that is needed if RCB is dynamic</param>
        /// <returns>Returns boolean value: true if entry is wrong and false if not</returns>

        private Boolean VerifyRCBItem(XPathNodeIterator privateRCBIterator, string address, string iedName, string dataset, string trgOpt, string confRev, string custom)
        {
            Boolean skipRCBItem = false;
            Boolean duplicateDynDs = false;
            String[] splittedDataset;
            string dsDotted = "";
            string dynamicDs = "";

            if (!dataset.Contains("@"))
            {
                splittedDataset = dataset.Split('/', '.');
                dsDotted = splittedDataset[2] + "." + iedName + "." + splittedDataset[0].Replace(iedName, "") + "." + splittedDataset[1];
            }
            else
            {
                dynamicDs = dataset;
                if (!IdynamicDataSetsInUse.ContainsKey(dynamicDs) && custom == "true")
                {
                    IdynamicDataSetsInUse.Add(dynamicDs, dynamicDs);
                }
                else
                {
                    duplicateDynDs = true;
                }
            }

            if (String.IsNullOrEmpty(address)
                    || String.IsNullOrEmpty(iedName)
                    || String.IsNullOrEmpty(dataset)
                    || String.IsNullOrEmpty(trgOpt)
                    || String.IsNullOrEmpty(confRev)
                    || (!dataSets.ContainsKey(dsDotted) && !custom.Equals("true"))
                    || (!String.IsNullOrEmpty(dynamicDs) && !custom.Equals("true"))
                    || duplicateDynDs
                    || String.IsNullOrEmpty(FindReportFromAddress(address))
                    )
            {
                if (skipAllWrongItems == true)
                {
                    skipRCBItem = true;
                    MappingChanged = true;
                }
                else
                {
                    DialogResult ds = MessageBox.Show("Invalid Report Control Block entry:\n\n" + privateRCBIterator.Current.OuterXml.ToString() + "\n\nDo you want to skip this RCB entry with assigned Data Items?"
                                        , "Private section parsing error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (ds == DialogResult.Yes)
                    {
                        DialogResult dialRes = MessageBox.Show("Do you want to skip ALL invalid entries?"
                                        , "Private section parsing error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        if (dialRes == DialogResult.Yes)
                        {
                            skipRCBItem = true;
                            skipAllWrongItems = true;
                        }
                        else
                        {
                            skipAllWrongItems = false;
                            skipRCBItem = true;
                        }
                        MappingChanged = true;
                    }
                    if (ds == DialogResult.No)
                    {
                        wrongEntry = true;
                        dynamicDataSets.Clear();
                        dynamicDataSetsInUse.Clear();
                        usedRcb.Clear();
                        additionalTags.Clear();
                        iedMappings.Clear();
                        reports.Clear();
                        gridItems.Clear();
                        controlTags.Clear();
                        ieddinTags.Clear();
                        parameterTags.Clear();
                        analogTags.Clear();
                        localRcb.Clear();
                        dataSets.Clear();
                        lnDict.Clear();
                        lNodeTypeDict.Clear();
                        doTypeDict.Clear();
                        daTypesDict.Clear();

                        throw new System.ArgumentException("Report Control Block has at least one invalid or missing attribute.");
                    }
                }
            }

            return skipRCBItem;
        }

        /// <summary>
        /// Method that is used for validation DI entry. Validation contains attributes and syntax. 
        /// </summary>
        /// <param name="privateDiIterator">XML Iterator of selected DI entry</param>
        /// <param name="address">Address attribute that is assigned to selected DI entry</param>
        /// <param name="fc">Functional constrains attribute that is assigned to selected DI entry</param>
        /// <param name="iedName">IedName attribute that is assigned to selected DI entry</param>
        /// <param name="type">Type attribute that is assigned to selected DI entry</param>
        /// <param name="rType"> rType attribute that is assigned to selected DI entry</param>
        /// <param name="index">Index attribute that is assigned to selected DI entry</param>
        /// <returns>Returns boolean value: true if entry is wrong and false if not</returns>
        private Boolean VerifyDataItem(XPathNodeIterator privateDiIterator, string address, string fc, string iedName, string type, string rType, string index)
        {
            Boolean skipDIItem = false;

            if (String.IsNullOrEmpty(address)
                       || String.IsNullOrEmpty(fc)
                       || String.IsNullOrEmpty(iedName)
                       || String.IsNullOrEmpty(type)
                       || String.IsNullOrEmpty(rType)
                       || String.IsNullOrEmpty(index)
                       || (String.IsNullOrEmpty(FindReportFromAddress(rType)) && !rType.Equals("On Request")))
            {
                if (skipAllWrongItems == true)
                {
                    skipDIItem = true;
                    MappingChanged = true;

                }
                else
                {
                    DialogResult drs = MessageBox.Show("Invalid Data Item entry:\n\n" + privateDiIterator.Current.OuterXml.ToString() + "\n\nDo you want to skip this entry?"
                                        , "Private section parsing error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    if (drs == DialogResult.Yes)
                    {
                        DialogResult dialRes = MessageBox.Show("Do you want to skip ALL invalid entries?"
                                         , "Private section parsing error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        if (dialRes == DialogResult.Yes)
                        {
                            skipAllWrongItems = true;
                            skipDIItem = true;
                        }
                        else
                        {
                            skipAllWrongItems = false;
                            skipDIItem = true;
                        }
                        MappingChanged = true;
                    }
                    if (drs == DialogResult.No)
                    {

                        skipAllWrongItems = false;
                        wrongEntry = true;
                        dynamicDataSets.Clear();
                        dynamicDataSetsInUse.Clear();
                        usedRcb.Clear();
                        additionalTags.Clear();
                        iedMappings.Clear();
                        reports.Clear();
                        gridItems.Clear();
                        controlTags.Clear();
                        ieddinTags.Clear();
                        parameterTags.Clear();
                        analogTags.Clear();
                        localRcb.Clear();
                        dataSets.Clear();
                        lnDict.Clear();
                        lNodeTypeDict.Clear();
                        doTypeDict.Clear();
                        daTypesDict.Clear();

                        throw new System.ArgumentException("Data Item entry has at least one invalid or missing attribute.");
                    }
                }
            }

            return skipDIItem;
        }

        /// <summary>
        /// SubMethod that parse specifeid LN 
        /// </summary>
        /// <param name="logicalDeviceName">LDevice Name</param>
        /// <param name="IEDname">IED name</param>
        /// <param name="lDevice">XPath iterator for specified LDevice</param>
        private void ParseLn(string logicalDeviceName, string IEDname, XPathNodeIterator lDevice)
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator ln = lDevice.Current.SelectDescendants("LN0", navigator.NamespaceURI, false);

            GetLn(logicalDeviceName, IEDname, ln);
            ln = lDevice.Current.SelectDescendants("LN", navigator.NamespaceURI, false);
            GetLn(logicalDeviceName, IEDname, ln);

        }

        /// <summary>
        /// Sub method used for traversing specified LN
        /// </summary>
        /// <param name="LogicalDeviceName">LDevice name</param>
        /// <param name="IEDname">IED name</param>
        /// <param name="ln">XPath iterator to LN</param>
        private void GetLn(string LogicalDeviceName, string IEDname, XPathNodeIterator ln)
        {
            string LNClass = "";
            string lnName = "";
            while (ln.MoveNext())
            {
                string instLn = "";
                string prefix = "";
                string lnInst = "";
                string lnType = "";
                lnName = ln.Current.Name;
                if (ln.Current != null)
                {
                    XPathNavigator backup = ln.Current.Clone();
                    XPathNavigator backup2 = ln.Current.Clone();

                    backup.MoveToParent();
                    backup.MoveToFirstAttribute();
                    do
                    {
                        if (backup.Name == "inst")
                        {
                            instLn = backup.Value;
                        }
                    } while (backup.MoveToNextAttribute());

                    if (LogicalDeviceName != instLn)
                    {
                        continue;
                    }

                    if (ln.Current != null)
                    {
                        XPathNavigator nextBackup = ln.Current.Clone();

                        nextBackup.MoveToFirstAttribute();

                        do
                        {
                            if (nextBackup.Name == "prefix")
                            {
                                prefix = nextBackup.Value;
                            }
                            if (nextBackup.Name == "lnClass")
                            {
                                LNClass = nextBackup.Value;
                            }
                            if (nextBackup.Name == "inst")
                            {
                                lnInst = nextBackup.Value;
                            }
                            if (nextBackup.Name == "lnType")
                            {
                                lnType = nextBackup.Value;
                            }
                        } while (nextBackup.MoveToNextAttribute());

                        string lnCheck = IEDname + LogicalDeviceName + prefix + LNClass + lnInst;

                        if (lnInst == "" && lnName == "LN")
                        {
                            MessageBox.Show("Logical node inst value is empty.\nPlease check SCL file", "SCL parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            CorruptedScl = true;
                            break;
                        }

                        try
                        {
                            lnDict.Add(lnCheck, new LN(lnType, LNClass, prefix, lnInst, IEDname, LogicalDeviceName));
                        }
                        catch (System.ArgumentException)
                        {
                            throw new System.ArgumentException("Duplicated Logical Node entry: " + LogicalDeviceName);
                        }



                        //check instances in LN
                        XPathNodeIterator doiIterator = backup2.SelectDescendants("DOI", backup2.NamespaceURI, false);
                        while (doiIterator.MoveNext())
                        {
                            XPathNavigator doiBack = doiIterator.Current.Clone();
                            doiBack.MoveToFirstAttribute();
                            string doiName = "";
                            do
                            {
                                if (doiBack.Name == "name")
                                {
                                    doiName = doiBack.Value;
                                }
                            } while (doiBack.MoveToNextAttribute());
                            XPathNodeIterator dai = doiIterator.Current.SelectDescendants("DAI", backup2.NamespaceURI, false);
                            while (dai.MoveNext())
                            {
                                XPathNavigator back = dai.Current.Clone();
                                back.MoveToFirstAttribute();
                                //string name = "";
                                do
                                {
                                    if (back.Name == "name" && back.Value == "ctlModel")
                                    {
                                        //find control model
                                        string model = "";
                                        XPathNodeIterator ctl = dai.Clone();
                                        ctl = ctl.Current.SelectDescendants("Val", backup2.NamespaceURI, false);
                                        while (ctl.MoveNext())
                                        {
                                            model = ctl.Current.Value;
                                        }
                                        if (model != "")
                                        {
                                            if (lNodeTypeDict.ContainsKey(lnType))
                                            {
                                                LNType l = lNodeTypeDict[lnType];

                                                if (l.DataObjects.ContainsKey(doiName))
                                                {
                                                    string t = l.DataObjects[doiName];
                                                    if (doTypeDict.ContainsKey(t))
                                                    {
                                                        Dictionary<string, Ditem> dic = doTypeDict[t];
                                                        foreach (var v in dic)
                                                        {
                                                            if (v.Key.Contains("ctlModel"))
                                                            {
                                                                v.Value.ctlModel = model;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                } while (back.MoveToNextAttribute());
                            }
                        }
                    }
                }
                XPathNodeIterator copy = ln.Clone();
                ParseDataSets(copy, IEDname, LogicalDeviceName, prefix + LNClass + lnInst);
                ParseReports(copy, IEDname, LogicalDeviceName, prefix + LNClass + lnInst);
            }
        }
        /// <summary>
        /// Submethod thats parse DA types parameters
        /// </summary>
        private void ParseDATypes()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator DATypes = navigator.SelectDescendants("DAType", navigator.NamespaceURI, false);
            string name = "";
            string bType = "";
            string type = "";
            string model = "";

            while (DATypes.MoveNext())
            {
                XPathNodeIterator tmp = DATypes.Clone();
                tmp.Current.MoveToFirstAttribute();
                string DATypeID = "";
                do
                {
                    if (tmp.Current.Name == "id")
                    {
                        DATypeID = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                XPathNodeIterator tmp2 = DATypes.Clone();
                tmp2 = tmp2.Current.SelectDescendants("BDA", navigator.NamespaceURI, false);

                var dict = new Dictionary<string, Bda>();
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();

                    tmp3.Current.MoveToFirstAttribute();
                    name = "";
                    bType = "";
                    type = "";
                    model = "";
                    do
                    {
                        if (tmp3.Current.Name == "name")
                        {
                            name = tmp3.Current.Value;
                        }
                        if (tmp3.Current.Name == "bType")
                        {
                            bType = tmp3.Current.Value;
                        }
                        if (tmp3.Current.Name == "type")
                        {
                            type = tmp3.Current.Value;
                        }
                    } while (tmp3.Current.MoveToNextAttribute());

                    if (name == "ctlModel")
                    {
                        //find control model
                        XPathNodeIterator ctl = tmp3.Clone();
                        ctl.Current.MoveToParent();
                        ctl = ctl.Current.SelectDescendants("Val", navigator.NamespaceURI, false);
                        while (ctl.MoveNext())
                        {
                            model = ctl.Current.Value;
                        }
                    }

                    if (name.Contains("ctlVal"))
                    {
                        try
                        {
                            dict.Add(name + "." + idDi, new Bda(idDi, name, bType, type));
                            idDi++;
                            dict.Add(name + "." + idDi, new Bda(idDi, name, bType, type));
                        }
                        catch (System.ArgumentException)
                        {
                            throw new System.ArgumentException("Duplicated BDA entry: " + name);
                        }
                    }
                    else
                    {
                        try
                        {
                            dict.Add(name + "." + idDi, new Bda(idDi, name, bType, type));
                        }
                        catch (System.ArgumentException)
                        {
                            throw new System.ArgumentException("Duplicated BDA entry: " + name);
                        }
                        idDi++;
                    }
                }
                IdaTypesDict.Add(DATypeID, dict);
            }
        }
        //Namrata:14/03/2018 
        private void ParserRemoteIP()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator LNodeType = navigator.SelectDescendants("Communication", navigator.NamespaceURI, false);
            while (LNodeType.MoveNext())
            {
                XPathNodeIterator tmp = LNodeType.Clone();
                tmp.Current.MoveToFirstAttribute();
                XPathNodeIterator tmp2 = LNodeType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("P", navigator.NamespaceURI, false);
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    string RemoteIP = "";
                    if(tmp3.Current.Value == "IP")
                    {
                        if (tmp2.Current.Name == "P")
                        {
                            RemoteIP = tmp2.Current.Value;
                            Utils.RemoteIPAddress = RemoteIP;
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        private void ParserIEDName()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator LNodeType = navigator.SelectDescendants("Communication", navigator.NamespaceURI, false);
            while (LNodeType.MoveNext())
            {
                XPathNodeIterator tmp = LNodeType.Clone();
                tmp.Current.MoveToFirstAttribute();
                XPathNodeIterator tmp2 = LNodeType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("ConnectedAP", navigator.NamespaceURI, false);
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    string IEDName = "";
                    if(tmp3.CurrentPosition == 1)
                    {
                        if (tmp3.Current.Name == "iedName")
                        {
                                IEDName = tmp3.Current.Value;
                                Utils.FinalIEDName = IEDName;
                        }
                    }
                    else { }
                }
            }
        }
        public void TmpParseLnodeType()
        {
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator LNodeType = navigator.SelectDescendants("LNodeType", navigator.NamespaceURI, false);

            while (LNodeType.MoveNext())
            {
                XPathNodeIterator tmp = LNodeType.Clone();
                tmp.Current.MoveToFirstAttribute();

                string lnTypeId = "";
                do
                {
                    if (tmp.Current.Name == "id")
                    {
                        lnTypeId = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                LNType ln = new LNType();

                XPathNodeIterator tmp2 = LNodeType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("DO", navigator.NamespaceURI, false);
                while (tmp2.MoveNext())
                {
                    XPathNodeIterator tmp3 = tmp2.Clone();
                    tmp3.Current.MoveToFirstAttribute();
                    string name = "";
                    string type = "";
                    do
                    {
                        if (tmp3.Current.Name == "name")
                        {
                            name = tmp3.Current.Value;
                        }
                        if (tmp3.Current.Name == "type")
                        {
                            type = tmp3.Current.Value;
                        }
                    } while (tmp3.Current.MoveToNextAttribute());

                    try
                    {
                        ln.AddDO(name, type);
                    }
                    catch (System.ArgumentException)
                    {
                        throw new System.ArgumentException("Duplicated Data Object entry: " + name);
                    }
                }
                try
                {
                    lNodeTypeDict.Add(lnTypeId, ln);
                }
                catch (System.ArgumentException)
                {
                    throw new System.ArgumentException("Duplicated Logical Node Type entry: " + lnTypeId);
                }
            }

        }
        /// <summary>
        /// Method parse DOType parameters and create DA collection
        /// </summary>
        public void TmpParseDOtype()
        {
            string name = "";
            string type = "";
            string FC = "";
            string bType = "";
            string count = "";
            string model = "";
            XPathNavigator navigator = LastChild.CreateNavigator();
            XPathNodeIterator DOType = navigator.SelectDescendants("DOType", navigator.NamespaceURI, false);

            while (DOType.MoveNext())
            {
                Dictionary<string, Ditem> daDict = new Dictionary<string, Ditem>();

                XPathNodeIterator tmp = DOType.Clone();
                tmp.Current.MoveToFirstAttribute();
                string doTypeId = "";
                do
                {
                    if (tmp.Current.Name == "id")
                    {
                        doTypeId = tmp.Current.Value;
                    }
                } while (tmp.Current.MoveToNextAttribute());

                XPathNodeIterator tmp2 = DOType.Clone();
                tmp2 = tmp2.Current.SelectDescendants("DA", navigator.NamespaceURI, false);
                if (tmp2.Count > 0)
                {
                    while (tmp2.MoveNext())
                    {
                        XPathNodeIterator tmp3 = tmp2.Clone();
                        tmp3.Current.MoveToFirstAttribute();
                        name = "";
                        type = "";
                        FC = "";
                        bType = "";
                        count = "";
                        model = "";
                        do
                        {
                            if (tmp3.Current.Name == "name")
                            {
                                name = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "fc")
                            {
                                FC = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "bType")
                            {
                                bType = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "type")
                            {
                                type = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "count")
                            {
                                count = tmp3.Current.Value;
                            }
                        } while (tmp3.Current.MoveToNextAttribute());

                        if (name == "ctlModel")
                        {
                            //find control model
                            XPathNodeIterator ctl = tmp3.Clone();
                            ctl.Current.MoveToParent();
                            ctl = ctl.Current.SelectDescendants("Val", navigator.NamespaceURI, false);
                            while (ctl.MoveNext())
                            {
                                model = ctl.Current.Value;
                            }
                        }

                        if (!name.Equals("SBOw") &&
                            !name.Equals("Cancel"))
                        {
                            Ditem da = new Ditem(idDi, false, name, FC, bType, type, count, model);
                            try
                            {
                                daDict.Add(name + "." + idDi, da);
                            }
                            catch (System.ArgumentException)
                            {
                                throw new System.ArgumentException("Duplicated Data Item entry: " + name);
                            }
                            idDi++;

                        }

                    }
                }

                XPathNodeIterator tmp4 = DOType.Clone();
                tmp4 = tmp4.Current.SelectDescendants("SDO", navigator.NamespaceURI, false);

                if (tmp4.Count > 0)
                {
                    while (tmp4.MoveNext())
                    {
                        XPathNodeIterator tmp3 = tmp4.Clone();
                        tmp3.Current.MoveToFirstAttribute();
                        name = "";
                        type = "";
                        count = "";
                        do
                        {
                            if (tmp3.Current.Name == "name")
                            {
                                name = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "type")
                            {
                                type = tmp3.Current.Value;
                            }
                            if (tmp3.Current.Name == "count")
                            {
                                count = tmp3.Current.Value;
                            }
                        } while (tmp3.Current.MoveToNextAttribute());

                        if (name == "ctlModel")
                        {
                            //find control model
                            XPathNodeIterator ctl = tmp3.Clone();
                            ctl.Current.MoveToParent();
                            ctl = ctl.Current.SelectDescendants("Val", navigator.NamespaceURI, false);
                            while (ctl.MoveNext())
                            {
                                model = ctl.Current.Value;
                            }
                        }
                        try
                        {
                            Ditem da = new Ditem(idDi, true, name, "", "", type, count, model);
                            daDict.Add(name + "." + idDi, da);
                            idDi++;
                        }
                        catch (System.ArgumentException)
                        {
                            throw new System.ArgumentException("Duplicated Data Item entry: " + name);
                        }
                    }
                }
                try
                {
                    doTypeDict.Add(doTypeId, daDict);
                }
                catch (System.ArgumentException)
                {
                    throw new System.ArgumentException("Duplicated Data Object Type entry: " + doTypeId);
                }
            }
        }
        /// <summary>
        /// Method parse DataSets and create collection of Data Sets
        /// </summary>
        /// <param name="lNodeIterator">XPath iterator to Logical Node</param>
        /// <param name="ieDname">IED name</param>
        /// <param name="logicalDeviceName">Logical Device Name</param>
        /// <param name="lnName">LN name</param>
        public void ParseDataSets(XPathNodeIterator lNodeIterator, string ieDname, string logicalDeviceName, string lnName)
        {
            string ldInst = "";
            string prefix = "";
            string lnClass = "";
            string lnInst = "";
            string doName = "";
            string daName = "";
            string fc = "";

            XPathNodeIterator tmp = lNodeIterator.Current.SelectDescendants("DataSet", lNodeIterator.Current.NamespaceURI, false);
            while (tmp.MoveNext())
            {
                XPathNodeIterator DsName = tmp.Clone();
                DsName.Current.MoveToFirstAttribute();
                string name = "";

                do
                {
                    if (DsName.Current.Name == "name")
                    {
                        name = DsName.Current.Value;
                        break;
                    }
                } while (DsName.Current.MoveToNextAttribute());

                XPathNodeIterator fcdaIterator = tmp.Current.SelectDescendants("FCDA", lNodeIterator.Current.NamespaceURI, false);
                List<Fcda> list = new List<Fcda>();

                while (fcdaIterator.MoveNext())
                {
                    XPathNodeIterator tmp2 = fcdaIterator.Clone();

                    ldInst = "";
                    prefix = "";
                    lnClass = "";
                    lnInst = "";
                    doName = "";
                    daName = "";
                    fc = "";


                    tmp2.Current.MoveToFirstAttribute();

                    do
                    {
                        if (tmp2.Current.Name == "ldInst")
                        {
                            ldInst = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "prefix")
                        {
                            prefix = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "lnClass")
                        {
                            lnClass = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "lnInst")
                        {
                            lnInst = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "doName")
                        {
                            doName = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "daName")
                        {
                            daName = tmp2.Current.Value;
                        }
                        if (tmp2.Current.Name == "fc")
                        {
                            fc = tmp2.Current.Value;
                        }
                    } while (tmp2.Current.MoveToNextAttribute());
                    try
                    {
                        list.Add(new Fcda(ieDname, name, ldInst, prefix, lnClass, lnInst, doName, daName, fc));
                    }
                    catch (System.ArgumentException)
                    {
                        throw new System.ArgumentException("Duplicated FCDA entry: " + name);
                    }
                }
                try
                {
                    dataSets.Add(name + "." + ieDname + "." + logicalDeviceName + "." + lnName, list);
                }
                catch (System.ArgumentException)
                {
                    throw new System.ArgumentException("Duplicated Data Set entry: " + name);
                }
            }
        }
        /// <summary>
        /// Method parse ReportControl section amd create Report collection
        /// </summary>
        /// <param name="nodeIterator">XPath iterator for specified node</param>
        /// <param name="ieDname">IED name</param>
        /// <param name="lDevName">Logical Device Name</param>
        /// <param name="lnName">LN name</param>
        public void ParseReports(XPathNodeIterator nodeIterator, string ieDname, string lDevName, string lnName)
        {
            if (nodeIterator.Current != null)
            {
                string name = "";
                string dSet = "";
                string iPd = "";
                string rId = "";
                string cRev = "";
                string buff = "";
                string bTime = "";
                string custom = "";
                string trigOpt = "";
                string reportName = "";
                string datasetAddress = "";
                string maxRptEnabled = "";
                string seqNum = "";
                string timeStamp = "";
                string dataSet = "";
                string dataRef = "";
                string configRef = "";
                string segmentation = "";
                string dchg = "";
                string qchg = "";
                string dupd = "";
                string period = "";
                string gi = "";
                string tempVar = "";
                string tempVarName = "";
                string tempVarId = "";

                int maxIntRptEnabled = 0;

                XPathNodeIterator rIterator = nodeIterator.Current.SelectDescendants("ReportControl", nodeIterator.Current.NamespaceURI, false);
                while (rIterator.MoveNext())
                {
                    XPathNodeIterator rIteratorOptF = rIterator.Clone();
                    XPathNodeIterator rIteratorTrigOpt = rIterator.Clone();
                    XPathNodeIterator tmp = rIterator.Clone();
                    name = "";
                    dSet = "";
                    iPd = "";
                    rId = "";
                    cRev = "";
                    buff = "";
                    bTime = "";
                    custom = "";
                    trigOpt = "";
                    maxRptEnabled = "";
                    tmp.Current.MoveToFirstAttribute();


                    do
                    {
                        if (tmp.Current.Name.Equals("name")) { name = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("datSet")) { dSet = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("intgPd")) { iPd = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("rptID")) { rId = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("confRev")) { cRev = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("buffered")) { buff = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("bufTime")) { bTime = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("custom")) { custom = tmp.Current.Value; }
                        if (tmp.Current.Name.Equals("triggerOptions")) { trigOpt = tmp.Current.Value; }
                    } while (tmp.Current.MoveToNextAttribute());



                    XPathNodeIterator optFieldsIterator = rIteratorOptF.Current.SelectDescendants("OptFields", nodeIterator.Current.NamespaceURI, false);
                    XPathNodeIterator tmp1;
                    XPathNodeIterator tmp2;
                    XPathNodeIterator tmp3;

                    XPathNodeIterator RptEnabledIterator = rIteratorTrigOpt.Current.SelectDescendants("RptEnabled", nodeIterator.Current.NamespaceURI, false);

                    while (RptEnabledIterator.MoveNext())
                    {
                        tmp3 = RptEnabledIterator.Clone();
                        tmp3.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp3.Current.Name.Equals("max"))
                            {
                                maxRptEnabled = tmp3.Current.Value;
                            }
                        } while (tmp3.Current.MoveToNextAttribute());
                    }
                    if (!String.IsNullOrEmpty(maxRptEnabled))
                    {
                        maxIntRptEnabled = Convert.ToInt32(maxRptEnabled);
                    }
                    else
                    {
                        maxIntRptEnabled = 0;
                    }

                    while (optFieldsIterator.MoveNext())
                    {
                        tmp1 = optFieldsIterator.Clone();
                        tmp1.Current.MoveToFirstAttribute();

                        do
                        {
                            if (tmp1.Current.Name.Equals("seqNum"))
                            {
                                seqNum = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("timeStamp"))
                            {
                                timeStamp = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("dataSet"))
                            {
                                dataSet = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("dataRef"))
                            {
                                dataRef = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("configRef"))
                            {
                                configRef = tmp1.Current.Value;
                            }
                            if (tmp1.Current.Name.Equals("segmentation"))
                            {
                                segmentation = tmp1.Current.Value;
                            }
                        } while (tmp1.Current.MoveToNextAttribute());
                    }

                    XPathNodeIterator TrgOpsIterator = rIteratorTrigOpt.Current.SelectDescendants("TrgOps", nodeIterator.Current.NamespaceURI, false);

                    while (TrgOpsIterator.MoveNext())
                    {
                        tmp2 = TrgOpsIterator.Clone();
                        tmp2.Current.MoveToFirstAttribute();
                        do
                        {
                            if (tmp2.Current.Name.Equals("dchg"))
                            {
                                dchg = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("qchg"))
                            {
                                qchg = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("dupd"))
                            {
                                dupd = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("period"))
                            {
                                period = tmp2.Current.Value;
                            }
                            if (tmp2.Current.Name.Equals("gi"))
                            {
                                gi = tmp2.Current.Value;
                            }
                        } while (tmp2.Current.MoveToNextAttribute());
                    }

                    reportName = ieDname + lDevName + "/" + lnName + "." + name;
                    if (dSet == "")
                    {
                        custom = "true";
                        datasetAddress = "";
                    }
                    else
                    {
                        datasetAddress = ieDname + lDevName + "/" + lnName + "." + dSet;
                    }

                    if (maxIntRptEnabled < 100 && maxIntRptEnabled > 0)
                    {
                        for (int i = 1; i <= maxIntRptEnabled; i++)
                        {
                            try
                            {
                                if (i < 10)
                                {
                                    tempVar = reportName + "0" + i.ToString();
                                    tempVarName = name + "0" + i.ToString();
                                    tempVarId = rId + "0" + i.ToString();
                                }
                                else
                                {
                                    tempVar = reportName + i.ToString();
                                    tempVarName = name + i.ToString();
                                    tempVarId = rId + i.ToString();
                                }

                                Report report = new Report(tempVar, ieDname, lDevName, lnName, tempVarName, dSet, datasetAddress, iPd, tempVarId, cRev, buff, bTime, trigOpt, custom);

                                if (!String.IsNullOrEmpty(dchg))
                                {
                                    report.TrgOps["dchg"] = dchg;
                                }
                                if (!String.IsNullOrEmpty(qchg))
                                {
                                    report.TrgOps["qchg"] = qchg;
                                }
                                if (!String.IsNullOrEmpty(dupd))
                                {
                                    report.TrgOps["dupd"] = dupd;
                                }
                                if (!String.IsNullOrEmpty(period))
                                {
                                    report.TrgOps["period"] = period;
                                }
                                if (!String.IsNullOrEmpty(gi))
                                {
                                    report.TrgOps["gi"] = gi;
                                }

                                if (!String.IsNullOrEmpty(seqNum))
                                {
                                    report.OptFields["seqNum"] = seqNum;
                                }
                                if (!String.IsNullOrEmpty(timeStamp))
                                {
                                    report.OptFields["timeStamp"] = timeStamp;
                                }
                                if (!String.IsNullOrEmpty(dataSet))
                                {
                                    report.OptFields["dataSet"] = dataSet;
                                }
                                if (!String.IsNullOrEmpty(dataRef))
                                {
                                    report.OptFields["dataRef"] = dataRef;
                                }
                                if (!String.IsNullOrEmpty(configRef))
                                {
                                    report.OptFields["configRef"] = configRef;
                                }
                                if (!String.IsNullOrEmpty(segmentation))
                                {
                                    report.OptFields["segmentation"] = segmentation;
                                }
                                if (dSet != "")
                                {
                                    string check = dSet + "." + ieDname + "." + lDevName + "." + lnName;
                                    if (!this.dataSets.ContainsKey(check))
                                    {
                                        MessageBox.Show("Invalid dataset defined for " + reportName + ".\nPlease check SCL file", "SCL parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                                       MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    }
                                    else
                                    {
                                        reports.Add(name + "0" + i.ToString() + "." + ieDname + "." + lDevName + "." + lnName, report);
                                    }
                                }
                                else
                                {
                                    reports.Add(name + "0" + i.ToString() + "." + ieDname + "." + lDevName + "." + lnName, report);
                                }
                            }
                            catch (System.ArgumentException)
                            {
                                throw new System.ArgumentException("Duplicated Report entry: " + name);
                            }
                        }
                    }
                    else
                    {

                        try
                        {
                            Report report = new Report(reportName, ieDname, lDevName, lnName, name, dSet, datasetAddress, iPd, rId, cRev, buff, bTime, trigOpt, custom);
                            if (!String.IsNullOrEmpty(dchg))
                            {
                                report.TrgOps["dchg"] = dchg;
                            }
                            if (!String.IsNullOrEmpty(qchg))
                            {
                                report.TrgOps["qchg"] = qchg;
                            }
                            if (!String.IsNullOrEmpty(dupd))
                            {
                                report.TrgOps["dupd"] = dupd;
                            }
                            if (!String.IsNullOrEmpty(period))
                            {
                                report.TrgOps["period"] = period;
                            }
                            if (!String.IsNullOrEmpty(gi))
                            {
                                report.TrgOps["gi"] = gi;
                            }

                            string check = dSet + "." + ieDname + "." + lDevName + "." + lnName;

                            if (dSet == "")
                            {
                                custom = "true";
                                report.DSAddress = "";
                                report.Custom = "true";
                                report.DSAddressDots = "";
                            }
                            else
                            {
                                datasetAddress = ieDname + lDevName + "/" + lnName + "." + dSet;
                                report.DSAddress = datasetAddress;
                            }
                            if (!this.dataSets.ContainsKey(check) && report.Custom != "true")
                            {
                                MessageBox.Show("Invalid dataset defined for " + reportName + ".\nPlease check SCL file", "SCL parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                               MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            }
                            else
                            {
                                reports.Add(name + "." + ieDname + "." + lDevName + "." + lnName, report);
                            }
                        }
                        catch (System.ArgumentException)
                        {
                            throw new System.ArgumentException("Duplicated Report entry: " + name);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Method search a maching Reports.Key from specified address (Reports.Value.Address)
        /// </summary>
        /// <param name="addr">Report Address</param>
        /// <returns>maching report key from Reports collection</returns>
        public string FindReportFromAddress(string addr)
        {
            string ret = "";

            foreach (var rep in Ireports)
            {
                if (rep.Value.Address.Equals(addr))
                {
                    ret = rep.Key;
                    break;
                }
            }
            return ret;
        }



    }
    public class Report
    {
        public string Address { get; set; }
        public string ReportName { get; set; }
        public string DSName { get; set; }
        public string DSAddress { get; set; }
        public string DSAddressDots { get; set; }
        public string IntgPD { get; set; }
        /// <summary>
        /// Report address with replaced '.' to '$'
        /// </summary>
        public string RptId { get; set; }
        /// <summary>
        /// Configure revision number
        /// </summary>
        public string ConfRev { get; set; }
        /// <summary>
        /// Is item buffered
        /// </summary>
        public string Buffered { get; set; }
        /// <summary>
        /// Buffer time value
        /// </summary>
        public string BufTime { get; set; }
        /// <summary>
        /// Trigger Options in Numerical form
        /// </summary>
        public string TrgOptNum { get; set; }
        /// <summary>
        /// Custom field value
        /// </summary>
        public string Custom { get; set; }
        /// <summary>
        /// Assigned IED name 
        /// </summary>
        public string IedName { get; set; }
        /// <summary>
        /// Assigned Logical Device name
        /// </summary>
        public string LdeviceName { get; set; }
        /// <summary>
        /// Assigned logical node name
        /// </summary>
        public string LNName { get; set; }
        /// <summary>
        /// Trigger Options dictionary for enum form
        /// </summary>
        public Dictionary<string, string> TrgOps { get; private set; }
        /// <summary>
        /// Option fields dictionary for enum form
        /// </summary>
        public Dictionary<string, string> OptFields { get; private set; }
        /// <summary>
        /// Report object constructor - create object and initialize specified variables
        /// </summary>
        /// <param name="addr">Report addres</param>
        /// <param name="iedName">IED name</param>
        /// <param name="ldev">Logical Device Name</param>
        /// <param name="lnName">LN name</param>
        /// <param name="name">Raport name</param>
        /// <param name="dName">Raport Data Sets name</param>
        /// <param name="dAddress">Raport Data Set full path</param>
        /// <param name="period">Integrity period</param>
        /// <param name="rId">Raport ID</param>
        /// <param name="rev">Revision number</param>
        /// <param name="buf">Is report buffered</param>
        /// <param name="bTime">Buffering Time</param>
        /// <param name="triggerOptNum">Trigger options in Number form</param>
        /// <param name="cstm">Custom value</param>
        public Report(string addr, string iedName, string ldev, string lnName, string name, string dName, string dAddress, string period, string rId, string rev, string buf, string bTime, string triggerOptNum, string cstm)
        {
            IntgPD = "0";
            BufTime = "0";
            TrgOptNum = "0";

            Address = addr;
            ReportName = name;
            DSAddress = dAddress;
            DSAddressDots = dName + "." + iedName + "." + ldev + "." + lnName;
            DSName = dName;
            if (period.Length != 0) { IntgPD = period; }
            RptId = rId;
            ConfRev = rev;
            Buffered = buf;
            if (bTime.Length != 0) { BufTime = bTime; }
            this.IedName = iedName;
            LdeviceName = ldev;
            this.LNName = lnName;
            if (triggerOptNum.Length != 0) { TrgOptNum = triggerOptNum; }
            Custom = cstm;

            TrgOps = new Dictionary<string, string> {
            {"dchg","false"},
            {"qchg","false"},
            {"dupd","false"},
            {"period","false"},
            {"gi","false"}
        };

            OptFields = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// BDA class
    /// </summary>
    public class Bda
    {
        /// <summary>
        /// BDA constructor
        /// </summary>
        /// <param name="id">BDA id number</param>
        /// <param name="name">BDA name</param>
        /// <param name="bType">BDA bType</param>
        /// <param name="type">BDA type</param>
        public Bda(int id, string name, string bType, string type)
        {
            Id = id;
            Name = name;
            BType = bType;
            BdaType = type;
        }
        /// <summary>
        /// BDA internal Id number
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        ///BDA type 
        /// </summary>
        public string BdaType { get; private set; }
        /// <summary>
        /// BDA bType
        /// </summary>
        public string BType { get; private set; }
        /// <summary>
        /// BDA name
        /// </summary>
        public string Name { get; private set; }
    }
    /// <summary>
    /// DI item class
    /// </summary>
    public class Ditem
    {
        /// <summary>
        /// DI item constructor
        /// </summary>
        /// <param name="id">Data Item internal ID number</param>
        /// <param name="sda">is Data Item SDA</param>
        /// <param name="name">Data Item Name</param>
        /// <param name="fc">Data Item functional constrains</param>
        /// <param name="bType">Data Item bType</param>
        /// <param name="type">Data Item type</param>
        /// <param name="count">Data Item count</param>
        /// <param name="ctlmodel">Control model</param>
        public Ditem(int id, bool sda, string name, string fc, string bType, string type, string count, string ctlmodel)
        {
            Id = id;
            Sdo = sda;
            Name = name;
            FC = fc;
            BType = bType;
            DIType = type;
            Count = count;
            ctlModel = ctlmodel;
        }

        /// <summary>
        /// Data item internal ID number
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Is Data Item SDO
        /// </summary>
        public bool Sdo { get; private set; }
        /// <summary>
        /// Data Item functional constrains
        /// </summary>
        public string FC { get; private set; }
        /// <summary>
        /// Data Item type
        /// </summary>
        public string DIType { get; private set; }
        /// <summary>
        /// Data Item bType
        /// </summary>
        public string BType { get; private set; }
        /// <summary>
        /// Data Item name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Data Item count
        /// </summary>
        public string Count { get; private set; }
        /// <summary>
        /// Control model
        /// </summary>
        public string ctlModel { get; set; }
    }

    /// <summary>
    /// LN Type class
    /// </summary>
    public class LNType
    {
        private Dictionary<string, string> dataObjects;
        /// <summary>
        /// LNType initialization
        /// </summary>
        public LNType()
        {
            dataObjects = new Dictionary<string, string>();
        }
        /// <summary>
        ///	 Public access to Data Objects dictionary
        /// </summary>
        public Dictionary<string, string> DataObjects
        {
            get
            {
                return dataObjects;
            }
        }
        /// <summary>
        /// Method that add DO to collection
        /// </summary>
        /// <param name="name">DO name</param>
        /// <param name="type">DO type</param>
        public void AddDO(string name, string type)
        {
            if (!dataObjects.ContainsKey(name))
            {
                dataObjects.Add(name, type);
            }
        }
    }
    /// <summary>
    /// LN class
    /// </summary>
    public class LN
    {
        /// <summary>
        /// LN constructor
        /// </summary>
        /// <param name="lnType">LN type</param>
        /// <param name="lnClass">LN class</param>
        /// <param name="prefix">LN prefix</param>
        /// <param name="lnInstance">LN instance</param>
        /// <param name="iedName">IED name</param>
        /// <param name="ldevideName">LDevice Name</param>
        public LN(string lnType, string lnClass, string prefix, string lnInstance, string iedName, string ldevideName)
        {
            LNType = lnType;
            LNClass = lnClass;
            Prefix = prefix;
            IedName = iedName;
            LDeviceName = ldevideName;
            LNInstance = lnInstance;
        }
        /// <summary>
        /// Logical Node Prefix
        /// </summary>
        public string Prefix { get; private set; }
        /// <summary>
        /// Logical Node Instance
        /// </summary>
        public string LNInstance { get; private set; }
        /// <summary>
        /// Logical Device Name
        /// </summary>
        public string LDeviceName { get; private set; }
        /// <summary>
        /// IED name
        /// </summary>
        public string IedName { get; private set; }
        /// <summary>
        /// Logical Node Class
        /// </summary>
        public string LNClass { get; private set; }
        /// <summary>
        /// Logical Node Type
        /// </summary>
        public string LNType { get; private set; }
    }
    /// <summary>
    /// Grid item class
    /// </summary>
    public class GridItem
    {
        public string ObjectReference { get; private set; } /// Grid item object reference
        public string FC { get; private set; }/// Grid item user-defined description
        public string Description { get; set; }  /// Grid item internal grid ID number
        public int Id { get; set; }     /// Grid item internal ID number
        public int IdDI { get; set; }/// Grid Item mapping index
        public string Index { get; set; } /// Grid Item isChecked flag to set state change
        public bool IsChecked { get; set; } /// Grid Item Logical Node name
        public string LogicalNodeName { get; private set; } /// Grid Item assigned mapping type
        public string MappingType { get; set; } /// Grid Item type
        public string GridItemType { get; private set; } /// Grid item IED name
        public string IedName { get; private set; }  /// Grid item user-defined refresh type
        public string RefreshType { get; set; }  /// Control model
        public string ControlModel { get; set; }
        public GridItem(int id, int idDI, bool selected, string type, string objRef, string fc, string desc, string ln, string iedName, string model)
        {
            IsChecked = selected;
            GridItemType = type;
            ObjectReference = objRef;
            FC = fc;
            Description = desc;
            Id = id;
            IdDI = idDI;
            LogicalNodeName = ln;
            IedName = iedName;
            MappingType = "";
            RefreshType = "";
            Index = "";
            ControlModel = model;

        }
    }

    public class Fcda
    {
        public string IedName { get; set; }
        public string LDInst { get; set; }
        public string Prefix { get; set; }
        public string LNClass { get; set; }
        public string LNInst { get; set; }
        public string DOName { get; set; }
        public string DAName { get; set; }
        public string FC { get; set; }
        public string Address { get; set; }
        public string DataSetName { get; set; }
        public Fcda(string ied, string dsn, string ld, string pfx, string ln, string lnIns, string doN, string daN, string fce)
        {
            IedName = ied;
            LDInst = ld;
            Prefix = pfx;
            LNClass = ln;
            LNInst = lnIns;
            DOName = doN;
            FC = fce;
            DAName = daN;
            DataSetName = dsn;

            if (daN.Length == 0)
            {
                Address = ied + ld + "/" + pfx + ln + lnIns + "." + doN;
            }
            else
            {
                Address = ied + ld + "/" + pfx + ln + lnIns + "." + doN + "." + daN;
            }

        }
    }
}






