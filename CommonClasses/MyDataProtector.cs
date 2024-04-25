using Microsoft.AspNetCore.DataProtection;

namespace CommonClasses;

//!!!!!this is just Empty class but we need to implement some token protection with some sign in key here!!!!!!
public class MyDataProtector : IDataProtector
{
    public IDataProtector CreateProtector(string purpose)
    {
        return new MyDataProtector();
    }

    public byte[] Protect(byte[] plaintext)
    {
        return plaintext;
    }

    public byte[] Unprotect(byte[] protectedData)
    {
        return protectedData;
    }
}