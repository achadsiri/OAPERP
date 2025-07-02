namespace EGPConnect.ApiModels;

public class ParamObj
{
    public int year { get; set; }
    public string keyword { get; set; }
    public string dept_code { get; set; }
    public string winner_tin { get; set; }
    public decimal budget_start { get; set; }
    public string budget_end { get; set; }
    public int offset { get; set; }
    public int limit { get; set; }
}