using System.CommandLine;
using Library.CLI.Commands;

namespace Library.CLI;

public class Root : RootCommand
{
    public Root()
    {
        AddCommand(new SyncReadonly());
    }
}