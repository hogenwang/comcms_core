using COMCMS.Common;
using COMCMS.Web.Controllers;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class PaymentAndCaptchaTests
    {
        [Fact]
        public void WxPaymentNotify_ValidPayload_IsAccepted()
        {
            var valid = wxpaymentController.IsValidNotifyPayload(
                returnCode: "SUCCESS",
                resultCode: "SUCCESS",
                notifyAppId: "wx123456",
                expectedAppId: "wx123456",
                notifyMchId: "1900000109",
                expectedMchId: "1900000109",
                outTradeNo: "ORDER202611240001",
                totalFeeRaw: "1990",
                out var feeFen);

            Assert.True(valid);
            Assert.Equal(1990, feeFen);
            Assert.True(wxpaymentController.IsAmountMatched(19.90m, feeFen));
        }

        [Theory]
        [InlineData("FAIL", "SUCCESS", "wx123", "wx123", "mch1", "mch1", "ORD1", "100")]
        [InlineData("SUCCESS", "FAIL", "wx123", "wx123", "mch1", "mch1", "ORD1", "100")]
        [InlineData("SUCCESS", "", "wx123", "wx123", "mch1", "mch1", "ORD1", "100")]
        [InlineData("SUCCESS", "SUCCESS", "wx_other", "wx123", "mch1", "mch1", "ORD1", "100")]
        [InlineData("SUCCESS", "SUCCESS", "wx123", "wx123", "mch_other", "mch1", "ORD1", "100")]
        [InlineData("SUCCESS", "SUCCESS", "wx123", "wx123", "mch1", "mch1", "", "100")]
        [InlineData("SUCCESS", "SUCCESS", "wx123", "wx123", "mch1", "mch1", "ORD1", "0")]
        [InlineData("SUCCESS", "SUCCESS", "wx123", "wx123", "mch1", "mch1", "ORD1", "-100")]
        [InlineData("SUCCESS", "SUCCESS", "wx123", "wx123", "mch1", "mch1", "ORD1", "abc")]
        public void WxPaymentNotify_InvalidPayload_IsRejected(
            string returnCode, string resultCode,
            string notifyAppId, string expectedAppId,
            string notifyMchId, string expectedMchId,
            string outTradeNo, string totalFeeRaw)
        {
            var valid = wxpaymentController.IsValidNotifyPayload(
                returnCode, resultCode, notifyAppId, expectedAppId,
                notifyMchId, expectedMchId, outTradeNo, totalFeeRaw, out _);

            Assert.False(valid);
        }

        [Theory]
        [InlineData(10.00, 1000, true)]
        [InlineData(0.01, 1, true)]
        [InlineData(10.00, 1, false)]
        [InlineData(10.00, 999, false)]
        [InlineData(0.00, 0, false)]
        [InlineData(-5.00, -500, false)]
        public void WxPaymentNotify_AmountCheck_MatchesExactFen(decimal expectedYuan, int actualFen, bool expected)
        {
            Assert.Equal(expected, wxpaymentController.IsAmountMatched(expectedYuan, actualFen));
        }

        [Fact]
        public void VerifyCodeHelper_ConstantTimeComparison_IsCaseInsensitiveAndRejectsEmpty()
        {
            Assert.True(VerifyCodeHelper.VerifyCodeValue("Ab3K", "ab3k"));
            Assert.True(VerifyCodeHelper.VerifyCodeValue("Ab3K", " AB3K "));
            Assert.False(VerifyCodeHelper.VerifyCodeValue("Ab3K", "Ab3X"));
            Assert.False(VerifyCodeHelper.VerifyCodeValue("", ""));
            Assert.False(VerifyCodeHelper.VerifyCodeValue("Ab3K", ""));
            Assert.False(VerifyCodeHelper.VerifyCodeValue(null, "Ab3K"));
        }

        [Fact]
        public void VerifyCodeHelper_GeneratesCryptoRandomCodesWithExpectedLength()
        {
            var helper = VerifyCodeHelper.GetSingleObj();
            var num = helper.CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            var mix = helper.CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode);

            Assert.InRange(num.Length, 4, 5);
            Assert.InRange(mix.Length, 4, 5);
        }
    }
}
