namespace EGPConnect.ApiModels;

public class RootDetail
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public string time { get; set; }
    public ParamObj param_obj { get; set; }
    public List<ContractItem> result { get; set; }
}