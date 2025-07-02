namespace EGPConnect.ApiModels;

public class ContractItem
{
    public string project_id { get; set; }
    public string project_name { get; set; }
    public string project_type_name { get; set; }
    public string dept_name { get; set; }
    public string dept_sub_name { get; set; }
    public string purchase_method_name { get; set; }
    public string purchase_method_group_name { get; set; }
    public string announce_date { get; set; }
    public string project_money { get; set; }
    public string price_build { get; set; }
    public string sum_price_agree { get; set; }
    public int budget_year { get; set; }
    public string transaction_date { get; set; }
    public string province { get; set; }
    public string district { get; set; }
    public string subdistrict { get; set; }
    public string project_status { get; set; }
    public ProjectLocation project_location { get; set; }
    public string geom { get; set; }
    public List<Contract> contract { get; set; }
}