using System;
using System.Collections.Generic;
using System.Linq;

namespace Salino.Utilities
{
    public class ZarinpalResult
    {
        #region نمایش پیغام های نتیجه پرداخت
        /// <summary>
        /// این متد یک ورودی گرفته و نتیجه پیغام را بر می گرداند
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public static string Status(string resultId)
        {
            string result = "";
            switch (resultId)
            {
                case "-1":
                    result = "اطلاعات ارسال شده ناقص است";
                    break;
                case "-2":
                    result = "و يا مرچنت كد پذيرنده صحيح نيست IP";
                    break;
                case "-3":
                    result = "با توجه به محدوديت هاي شاپرك امكان پرداخت با رقم درخواست شده ميسر نمي باشد";
                    break;
                case "-4":
                    result = "سطح تاييد پذيرنده پايين تر از سطح نقره اي است.";
                    break;
                case "-11":
                    result = "درخواست مورد نظر يافت نشد.";
                    break;
                case "-12":
                    result = "امكان ويرايش درخواست ميسر نمي باشد.";
                    break;
                case "-21":
                    result = "هيچ نوع عمليات مالي براي اين تراكنش يافت نشد";
                    break;
                case "-22":
                    result = "تراكنش نا موفق ميباشد.";
                    break;
                case "-33":
                    result = "رقم تراكنش با رقم پرداخت شده مطابقت ندارد.";
                    break;
                case "34":
                    result = "سقف تقسيم تراكنش از لحاظ تعداد يا رقم عبور نموده است";
                    break;
                case "40":
                    result = "اجازه دسترسي به متد مربوطه وجود ندارد.";
                    break;
                case "41":
                    result = "غيرمعتبر ميباشد. AdditionalData اطلاعات ارسال شده مربوط به";
                    break;
                case "42":
                    result = "مدت زمان معتبر طول عمر شناسه پرداخت بايد بين 30 دقيقه تا 45 روز مي باشد.";
                    break;
                case "54":
                    result = "درخواست مورد نظر آرشيو شده است.";
                    break;
                case "100":
                    result = "عمليات با موفقيت انجام گرديده است.";
                    break;
                case "101":
                    result = "تراكنش انجام شده است. PaymentVerification عمليات پرداخت موفق بوده و قبلا";
                    break;

                default:
                    result = "خطای نا مشخص";
                    break;

            }
            return result;
        }

        #endregion
    }
}