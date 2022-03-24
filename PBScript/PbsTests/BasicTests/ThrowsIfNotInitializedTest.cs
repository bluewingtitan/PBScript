using NUnit.Framework;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.ProgramElements;

namespace PbsTexts.BasicTests;

public class ThrowsIfNotInitializedTest
{
    [Test]
    public void Test_ThrowsIfNotInitializedRun()
    {
        var env = PbsEnvironment.WithAllDefaultRepositories();
        
        Assert.Throws<NotProperlyInitializedException>(() => new ActionElement().Execute(env));
        Assert.Throws<UnclosedBlockException>(() => new ElseElement().Execute(env));
        Assert.Throws<UnclosedBlockException>(() => new ElseIfElement().Execute(env));
        Assert.Throws<UnexpectedBlockEndException>(() => new EndElement().Execute(env));
        Assert.Throws<NotProperlyInitializedException>(() => new IfElement().Execute(env));
        Assert.Throws<NotProperlyInitializedException>(() => new RequestElement().Execute(env));
        Assert.Throws<NotProperlyInitializedException>(() => new VariableElement().Execute(env));
        Assert.Throws<NotProperlyInitializedException>(() => new WhileElement().Execute(env));
    }
    
    
    
    [Test]
    public void Test_ThrowsIfNotInitializedCheck()
    {
        Assert.Throws<NotProperlyInitializedException>(() => new ActionElement().ThrowIfNotValid());
        Assert.Throws<UnexpectedBlockEndException>(() => new ElseElement().ThrowIfNotValid());
        Assert.Throws<NotProperlyInitializedException>(() => new IfElement().ThrowIfNotValid());
        Assert.Throws<UnexpectedBlockEndException>(() => new EndElement().ThrowIfNotValid());
        Assert.Throws<InvalidRequestException>(() => new RequestElement().ThrowIfNotValid());
        Assert.Throws<NotProperlyInitializedException>(() => new VariableElement().ThrowIfNotValid());
        Assert.Throws<NotProperlyInitializedException>(() => new WhileElement().ThrowIfNotValid());
    }
}