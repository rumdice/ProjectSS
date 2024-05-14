using CoreLibrary;

class Program
{
    static void Main(string[] args)
    {

        string testInput = "aaaa";
        var res = testInput.PrintString();
        // 출력 방식은 이곳에서 결정
        Console.WriteLine(res);

        string testInput2 = "";
        var res2 = testInput2.PrintString();
        Console.WriteLine(res2);
    }
}