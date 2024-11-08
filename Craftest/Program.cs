// See https://aka.ms/new-console-template for more information
while (true)
{
    Thread.Sleep(TimeSpan.FromMinutes(10));

    // Might as well jump or dance or something.
    int Thing = Random.Shared.Next(0, 10);
    switch (Thing)
    {
        case 0:
            {
                InputSimulator.InputSimulator.SendKeyPressSequence(gameWindow, 
                break;
            }
    }
}