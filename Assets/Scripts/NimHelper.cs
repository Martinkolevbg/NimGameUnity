public class NimHelper
{
    public static int CalculateCoinsToRemove(int coinsLeft)
    {
        int coinsToTake = (coinsLeft -1) % 4;

        if(coinsToTake  == 0)
        {
            return 1;
        }   
        else{
            return coinsToTake;
        }
    }
}
