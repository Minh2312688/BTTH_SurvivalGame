using UnityEngine;

public class Blueprint 
{
    public string name;
    public string Req1;
    public string Req2;
    public int Req1amount;
    public int Req2amount;
    public int numOfRequirements;//Tổng số lượng yêu cầu
    public Blueprint(string name, string Req1, string Req2, int Req1amount, int Req2amount, int numOfRequirements)
    {
        this.name = name;
        this.Req1 = Req1;
        this.Req2 = Req2;
        this.Req1amount = Req1amount;
        this.Req2amount = Req2amount;
        this.numOfRequirements = numOfRequirements;
        
    }
}
