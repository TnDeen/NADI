﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Common
{
    public class MyConstant
    {
        public const String localeFR = "fr-FR";
        public const String slash = "/";
        public const String ConfirmCusCode = "CCC";
        public const String Role_Admin = "Admin";
        public const String Role_Manager = "Manager";
        public const String Role_Vendor = "Vendor";
        public const String Role_User = "User";
        public const String Base_Url = "http://www.jomrumahlelong.com/";
        public const String Base_Url_local = "http://localhost:44300/";
        //public const String Base_Url = "http://jomrumah.w24.wh-2.com/myapp/";
        public const String Web_Name = "jomrumahlelong.com";
        public const String last_office_noTel = "012 836 9219";
        public const String agent_noTel = "011 836 9219";
        public const String wasapApiContact = "https://api.whatsapp.com/send?phone=60128369219";
        public const String user_support_email = "support@" + Web_Name;
        public const String user_admin_email = "admin@" + Web_Name;
        public const String email_smtp = "mail." + Web_Name;
        //public const String email_smtp_temp = "m04.internetmailserver.net";
        public const Boolean enableEmail = false;


        public const String member_code = "LLG";
        public const String listing_count_code = "LSTGCOUNT";

        public const String property_img_base_url = "Content/img/property-type/";
        public const String property_img_default_url = property_img_base_url + "default/";
        public const String file_jpg = ".jpg";


        //listing type
        public const String LSTNG_TYPE_ACTIVE = "LSTNG_TYPE_01";
        public const String LSTNG_TYPE_EXPRD = "LSTNG_TYPE_02";
        public const String LSTNG_TYPE_PENDING = "LSTNG_TYPE_PNDNG";


        public const String SK_NEGERI = "NEGERI";
        public const String NEGERI_JHR = "01";
        public const String NEGERI_KDH = "02";
        public const String NEGERI_KLT = "03";
        public const String NEGERI_MLK = "04";
        public const String NEGERI_NS = "05";
        public const String NEGERI_PHG = "06";
        public const String NEGERI_PP = "07";
        public const String NEGERI_PRK = "08";
        public const String NEGERI_PRS = "09";
        public const String NEGERI_SEL = "10";
        public const String NEGERI_TRG = "11";
        public const String NEGERI_SBH = "12";
        public const String NEGERI_SRW = "13";
        public const String NEGERI_WP_KL = "14";
        public const String NEGERI_WP_LB = "15";
        public const String NEGERI_WP_PJ = "16";
        
        public static readonly int[] allLevel = {Level_1, Level_2, Level_3,
            Level_4, Level_5, Level_6, Level_7, Level_8, Level_9, Level_10, Level_11, Level_12};

        public const int Level_1 = 1;
        public const int Level_2 = 2;
        public const int Level_3 = 3;
        public const int Level_4 = 4;
        public const int Level_5 = 5;
        public const int Level_6 = 6;
        public const int Level_7 = 7;
        public const int Level_8 = 8;
        public const int Level_9 = 9;
        public const int Level_10 = 10;
        public const int Level_11 = 11;
        public const int Level_12 = 12;

        public const double Point_1 = 50.4;
        public const double Point_2 = 8.57;
        public const double Point_3 = 4.28;
        public const double Point_4 = 2.14;
        public const double Point_5 = 1.071;
        public const double Point_6 = 0.536;
        public const double Point_7 = 0.268;
        public const double Point_8 = 0.134;
        public const double Point_9 = 0.067;
        public const double Point_10 = 0.033;
        public const double Point_11 = 0.017;
        public const double Point_12 = 0.008;

        MyConstant()
        {

        }
    }
}