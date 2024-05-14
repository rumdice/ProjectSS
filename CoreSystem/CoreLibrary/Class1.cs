namespace CoreLibrary;

public static class StringLibrary
{
    public static bool StartsWithUpper(this string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return false;

        char ch = str[0];
        return char.IsUpper(ch);
    }


    /// 입력한 문자열을 그대로 리턴해주는 테스트 라이브러리 기능
    public static string PrintString(this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return "string is null or empty";
        }

        return str;
    }

}