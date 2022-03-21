﻿namespace PBScript.Interfaces;

public interface IPbsObject
{
    public string GetDocumentation()
    {
        return "No documentation supplied";
    }
        
    public IPbsValue ExecuteAction(string command, string parameter, IPbsEnvironment env);

    public string GetStringValue();

}