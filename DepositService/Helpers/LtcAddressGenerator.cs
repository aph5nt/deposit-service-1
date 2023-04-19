using NBitcoin;

namespace DepositService.Helpers;

public static class LtcAddressGenerator
{
     public static List<string> Generate(int count, string passphrase)
     {
          var network = NBitcoin.Altcoins.Litecoin.Instance.Mainnet;
          var mnemo = new Mnemonic(passphrase);
          var masterKey = mnemo.DeriveExtKey("password to be stored somewhere else");
          List<string> output = new();
          for (int i = 0; i < count; i++)
          {
               ExtKey key = masterKey.Derive((uint)i);
               var pubAddress = key.PrivateKey.PubKey.GetAddress(ScriptPubKeyType.Segwit, network);
               output.Add(pubAddress.ToString());
          }

          return output;
     }
}