[TestCase("text", new[] {"text"})]
[TestCase("hello world", new[] {"hello", "world"})]
// Вставляйте сюда свои тесты
[TestCase("", new string[0])]
[TestCase("a  b", new[] {"a", "b"})]
[TestCase("'single'", new[] {"single"})]
[TestCase("\"double\"", new[] {"double"})]
[TestCase("''", new[] {""})]
[TestCase("\"\"", new[] {""})]
[TestCase("a b", new[] {"a", "b"})]
[TestCase("'b c'", new[] {"b c"})]
[TestCase("'\"'", new[] {"\""})]
[TestCase("\"\'\"", new[] {"'"})]
[TestCase("\"\\\\\"", new[] {"\\"})]
[TestCase("'\\''", new[] {"'"})]
[TestCase("\"\\\"\"", new[] {"\""})]
[TestCase("\"unclosed", new[] {"unclosed"})]
[TestCase("  start", new[] {"start"})]
[TestCase("end  ", new[] {"end"})]
[TestCase("a\"b\"", new[] {"a", "b"})]
[TestCase("'a'c", new[] {"a", "c"})]
[TestCase("\"space at end ", new[] {"space at end "})]
public static void RunTests(string input, string[] expectedOutput)
{
    // Тело метода изменять не нужно
    Test(input, expectedOutput);
}
