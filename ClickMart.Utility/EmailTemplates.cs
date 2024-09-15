using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ClickMart.Utility
{
    public static class EmailTemplates
    {
        public static string ConfirmationEmail(string Url)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                    <head>
                    <meta charset='utf-8' />
                    <meta http-equiv='x-ua-compatible' content='ie=edge' />
                    <title>Email Confirmation</title>
                    <meta name='viewport' content='width=device-width, initial-scale=1' />
                    <style type='text/css'>
                        @media screen {{
                        @font-face {{
                            font-family: 'Source Sans Pro';
                            font-style: normal;
                            font-weight: 400;
                            src: local('Source Sans Pro Regular'),
                            url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
                        }}
                        @font-face {{
                            font-family: 'Source Sans Pro';
                            font-style: normal;
                            font-weight: 700;
                            src: local('Source Sans Pro Bold'),
                            url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
                        }}
                        }}
          
                        body, table, td, a {{
                        -ms-text-size-adjust: 100%;
                        -webkit-text-size-adjust: 100%;
                        }}

                        table, td {{
                        mso-table-rspace: 0pt;
                        mso-table-lspace: 0pt;
                        }}

                        img {{
                        -ms-interpolation-mode: bicubic;
                        }}

                        a[x-apple-data-detectors] {{
                        font-family: inherit !important;
                        font-size: inherit !important;
                        font-weight: inherit !important;
                        line-height: inherit !important;
                        color: inherit !important;
                        text-decoration: none !important;
                        }}

                        body {{
                        width: 100% !important;
                        height: 100% !important;
                        padding: 0 !important;
                        margin: 0 !important;
                        background-color: #f3f4f6;
                        }}

                        table {{
                        border-collapse: collapse !important;
                        }}

                        a {{
                        color: #1a82e2;
                        }}

                        img {{
                        height: auto;
                        line-height: 100%;
                        text-decoration: none;
                        border: 0;
                        outline: none;
                        }}

                        .email-container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        }}

                        .email-header {{
                        background-color: #ffffff;
                        padding: 40px 20px;
                        border-radius: 8px 8px 0 0;
                        text-align: center;
                        border-bottom: 3px solid #d4dadf;
                        }}

                        .email-header img {{
                        width: 90px;
                        }}

                        .email-body {{
                        background-color: #ffffff;
                        padding: 40px 20px;
                        border-radius: 0 0 8px 8px;
                        }}

                        .email-body h1 {{
                        font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                        font-size: 28px;
                        font-weight: 700;
                        margin: 0;
                        color: #333333;
                        }}

                        .email-body p {{
                        font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                        font-size: 16px;
                        line-height: 24px;
                        margin: 20px 0;
                        color: #555555;
                        }}

                        .email-button {{
                        background-color: #1a82e2;
                        border-radius: 6px;
                        text-align: center;
                        margin-top: 20px;
                        }}

                        .email-button a {{
                        display: inline-block;
                        padding: 16px 36px;
                        font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                        font-size: 16px;
                        color: #ffffff;
                        text-decoration: none;
                        border-radius: 6px;
                        font-weight: 700;
                        }}

                        .email-footer {{
                        padding: 20px;
                        text-align: center;
                        font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                        font-size: 14px;
                        line-height: 20px;
                        color: #888888;
                        }}
                    </style>
                    </head>
                    <body>
                    <div class='email-container'>
                        <div class='email-header'>
                            <img
                            src='https://res.cloudinary.com/df6ylojjq/image/upload/v1726342908/cgizvbzhz7ere26nibno.png'
                            alt='Logo'
                            />
                        </a>
                        </div>

                        <div class='email-body'>
                        <h1>Confirm Your Email Address</h1>
                        <p>Click the button below to confirm your email address. If you did not create an account, you can safely ignore this email.</p>
                        <div class='email-button'>
                            <a href='{Url}' target='_blank'>Confirm Email</a>
                        </div>
                        <p>If that doesn't work, copy and paste the following link in your browser:</p>
                        <p><a href='{Url}' target='_blank'>{Url}</a></p>
                        </div>

                        <div class='email-footer'>
                        <p>You received this email because we received a request for ClickMart for your account. If you didn't request this, you can safely delete this email.</p>
                        <p>Cheers,<br />ClickMart</p>
                        </div>
                    </div>
                    </body>
                </html>
                ";
        }

        public static string OrderConfirmation(User user,List<OrderDetails> orderDetails, ShippingMethod shippingMethod, decimal totalPrice, Address address)
        {
            var productRows = string.Join("", orderDetails.Select(od => $@"
                <tr>
                    <td style='padding: 10px; border-bottom: 1px solid #eee;'>{od.Product.Title}</td>
                    <td style='padding: 10px; border-bottom: 1px solid #eee;'>{od.qty}</td>
                    <td style='padding: 10px; border-bottom: 1px solid #eee;'>{od.Price:C}</td>
                </tr>
            "));

            return @$"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; color: #333;'>
                <h2 style='text-align: center; color: #4CAF50;'>Thank you for your order!</h2>
                <p style='font-size: 18px;'>Hi {user.UserName},</p>
                <p style='font-size: 16px;'>Your order has been confirmed and will be shipped to:</p>
                <div style='background-color: #f7f7f7; padding: 15px; border-radius: 5px;'>
                    <p><strong>Address:</strong> {address.UnitNumber}, {address.StreetNumber} {address.AddressLine1}, {address.City}, {address.Region}, {address.Country.Name}</p>
                    <p><strong>Postal Code:</strong> {address.PostalCode}</p>
                </div>

                <h3 style='margin-top: 30px; border-bottom: 2px solid #4CAF50;'>Order Details</h3>
                <table style='width: 100%; border-collapse: collapse;'>
                    <thead>
                        <tr>
                            <th style='text-align: left; padding: 10px; background-color: #4CAF50; color: white;'>Product</th>
                            <th style='text-align: left; padding: 10px; background-color: #4CAF50; color: white;'>Quantity</th>
                            <th style='text-align: left; padding: 10px; background-color: #4CAF50; color: white;'>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {productRows}
                    </tbody>
                </table>

                <p style='margin-top: 20px; font-size: 16px;'><strong>Shipping Method:</strong> {shippingMethod.Name} - {shippingMethod.Price:C}</p>
                <p style='font-size: 16px;'><strong>Total Price:</strong> {totalPrice:C}</p>

                <p style='text-align: center; margin-top: 30px;'>
                    <a href='#' style='display: inline-block; padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;'>View Your Order</a>
                </p>

                <p style='text-align: center; font-size: 12px; color: #aaa;'>If you have any questions, feel free to contact our support team.</p>
            </div>";
        }

        public static string OrderStatusUpdate(string invoice,User user,string Status, string expectedArrival)
        {
            // Define possible order states
            bool isApproved = Status == SD.StatusApproved;
            bool isProcessing = Status == SD.StatusInProcess;
            bool isShipped = Status == SD.StatusShipped;
            bool isArrived = Status == SD.StatusDelevered;

            return @$"
    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; color: #333;'>
        <h2 style='text-align: center; color: #4CAF50;'>Order Status Update</h2>
        <p style='font-size: 18px;'>Hi {user.UserName},</p>
        <p style='font-size: 16px;'>Your order <strong>{invoice}</strong> status has been updated.</p>

        <div style='background-color: #f7f7f7; padding: 15px; border-radius: 5px;'>
            <p><strong>Expected Arrival:</strong> {expectedArrival}</p>
        </div>

        <ul style='list-style-type: none; padding: 0; display: flex; justify-content: space-between; margin: 30px 0;'>
            <li style='text-align: center; flex-grow: 1;'>
                <div style='border-radius: 50%; width: 50px; height: 50px; background-color: {(isApproved ? "#4CAF50" : "#ddd")}; margin: 0 auto;'>
                    <i class='fas fa-check-circle' style='color: white; font-size: 24px; line-height: 50px;'></i>
                </div>
                <p style='margin-top: 10px; font-weight: bold;'>Order Approved</p>
            </li>
            <li style='text-align: center; flex-grow: 1;'>
                <div style='border-radius: 50%; width: 50px; height: 50px; background-color: {(isProcessing ? "#4CAF50" : "#ddd")}; margin: 0 auto;'>
                    <i class='fas fa-tasks' style='color: white; font-size: 24px; line-height: 50px;'></i>
                </div>
                <p style='margin-top: 10px; font-weight: bold;'>Processing</p>
            </li>
            <li style='text-align: center; flex-grow: 1;'>
                <div style='border-radius: 50%; width: 50px; height: 50px; background-color: {(isShipped ? "#4CAF50" : "#ddd")}; margin: 0 auto;'>
                    <i class='fas fa-box-open' style='color: white; font-size: 24px; line-height: 50px;'></i>
                </div>
                <p style='margin-top: 10px; font-weight: bold;'>Shipped</p>
            </li>
            <li style='text-align: center; flex-grow: 1;'>
                <div style='border-radius: 50%; width: 50px; height: 50px; background-color: {(isArrived ? "#4CAF50" : "#ddd")}; margin: 0 auto;'>
                    <i class='fas fa-home' style='color: white; font-size: 24px; line-height: 50px;'></i>
                </div>
                <p style='margin-top: 10px; font-weight: bold;'>Arrived</p>
            </li>
        </ul>

        <p style='text-align: center; margin-top: 30px;'>
            <a href='#' style='display: inline-block; padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;'>Track Your Order</a>
        </p>

        <p style='text-align: center; font-size: 12px; color: #aaa;'>If you have any questions, feel free to contact our support team.</p>
    </div>";
        }

    }

}
