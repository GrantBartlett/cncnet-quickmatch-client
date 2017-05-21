using System;
using System.Net;

public class CnCNetEventUserLoginError : EventArgs
{
    public string ErrorMessage { get; private set; }
    public string ErrorCode { get; private set; }

    public CnCNetEventUserLoginError(string errorMessage, HttpStatusCode errorCode)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode.ToString();
    }
}
