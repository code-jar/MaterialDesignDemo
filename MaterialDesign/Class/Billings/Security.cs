using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;

namespace MaterialDesign.Class.Billings
{
    public class Security
    {
        private static String TAG = "IABUtil/Security";
        private static String KEY_FACTORY_ALGORITHM = "RSA";
        private static String SIGNATURE_ALGORITHM = "SHA1withRSA";


        public static bool VerifyPurchase(String base64PublicKey, String signedData, String signature)
        {
            if (Android.Text.TextUtils.IsEmpty(signedData) || Android.Text.TextUtils.IsEmpty(base64PublicKey) ||
                    Android.Text.TextUtils.IsEmpty(signature))
            {
                Android.Util.Log.Error(TAG, "Purchase verification failed: missing data.");
                return false;
            }

            IPublicKey key = Security.GeneratePublicKey(base64PublicKey);
            return Security.Verify(key, signedData, signature);
        }

        public static IPublicKey GeneratePublicKey(String encodedPublicKey)
        {
            try
            {
                byte[] decodedKey = Android.Util.Base64.Decode(encodedPublicKey, Android.Util.Base64Flags.Default);
                KeyFactory keyFactory = KeyFactory.GetInstance(KEY_FACTORY_ALGORITHM);
                return keyFactory.GeneratePublic(new Java.Security.Spec.X509EncodedKeySpec(decodedKey));
            }
            catch (NoSuchAlgorithmException e)
            {
                throw new Java.Lang.RuntimeException(e);
            }
            catch (Exception e)
            {
                Android.Util.Log.Error(TAG, "Invalid key specification.");
                throw new Java.Lang.IllegalArgumentException(e.Message);
            }
        }

        public static bool Verify(IPublicKey publicKey, String signedData, String signature)
        {
            byte[] signatureBytes;
            try
            {
                signatureBytes = Android.Util.Base64.Decode(signature, Android.Util.Base64Flags.Default);
            }
            catch (Java.Lang.IllegalArgumentException e)
            {
                Android.Util.Log.Error(TAG, "Base64 decoding failed.");
                return false;
            }
            try
            {
                Signature sig = Signature.GetInstance(SIGNATURE_ALGORITHM);
                sig.InitVerify(publicKey);
                sig.Update(Convert.ToSByte(signedData));
                if (!sig.Verify(signatureBytes))
                {
                    Android.Util.Log.Error(TAG, "Signature verification failed.");
                    return false;
                }
                return true;
            }
            catch (NoSuchAlgorithmException e)
            {
                Android.Util.Log.Error(TAG, "NoSuchAlgorithmException.");
            }
            catch (InvalidKeyException e)
            {
                Android.Util.Log.Error(TAG, "Invalid key specification.");
            }
            catch (SignatureException e)
            {
                Android.Util.Log.Error(TAG, "Signature exception.");
            }
            return false;
        }

    }
}