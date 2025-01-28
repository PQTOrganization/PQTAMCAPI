--------------------------------------------------------
--  File created - Monday-December-12-2022   
--------------------------------------------------------
DROP PACKAGE "PQAMC"."AMC_ACCOUNT_CATEGORY";
DROP PACKAGE "PQAMC"."AMC_ANNUAL_INCOME";
DROP PACKAGE "PQAMC"."AMC_AREA";
DROP PACKAGE "PQAMC"."AMC_BANK";
DROP PACKAGE "PQAMC"."AMC_CITY";
DROP PACKAGE "PQAMC"."AMC_CONTACT_OWNERSHIP";
DROP PACKAGE "PQAMC"."AMC_COUNTRY";
DROP PACKAGE "PQAMC"."AMC_EDUCATION";
DROP PACKAGE "PQAMC"."AMC_GENDER";
DROP PACKAGE "PQAMC"."AMC_OCCUPATION";
DROP PACKAGE "PQAMC"."AMC_PROFESSION";
DROP PACKAGE "PQAMC"."AMC_RESIDENTIAL_STATUS";
DROP PACKAGE "PQAMC"."AMC_SOURCE_INCOME";
DROP PACKAGE "PQAMC"."AMC_USER_REFRESH_TOKEN";
DROP PACKAGE "PQAMC"."AMC_USER_SP";
DROP PACKAGE "PQAMC"."AMC_ACCOUNT_CATEGORY";
DROP PACKAGE "PQAMC"."AMC_ANNUAL_INCOME";
DROP PACKAGE "PQAMC"."AMC_AREA";
DROP PACKAGE "PQAMC"."AMC_BANK";
DROP PACKAGE "PQAMC"."AMC_CITY";
DROP PACKAGE "PQAMC"."AMC_CONTACT_OWNERSHIP";
DROP PACKAGE "PQAMC"."AMC_COUNTRY";
DROP PACKAGE "PQAMC"."AMC_EDUCATION";
DROP PACKAGE "PQAMC"."AMC_GENDER";
DROP PACKAGE "PQAMC"."AMC_OCCUPATION";
DROP PACKAGE "PQAMC"."AMC_PROFESSION";
DROP PACKAGE "PQAMC"."AMC_RESIDENTIAL_STATUS";
DROP PACKAGE "PQAMC"."AMC_SOURCE_INCOME";
DROP PACKAGE "PQAMC"."AMC_USER_REFRESH_TOKEN";
DROP PACKAGE "PQAMC"."AMC_USER_SP";
DROP PACKAGE BODY "PQAMC"."AMC_ACCOUNT_CATEGORY";
DROP PACKAGE BODY "PQAMC"."AMC_ANNUAL_INCOME";
DROP PACKAGE BODY "PQAMC"."AMC_AREA";
DROP PACKAGE BODY "PQAMC"."AMC_BANK";
DROP PACKAGE BODY "PQAMC"."AMC_CITY";
DROP PACKAGE BODY "PQAMC"."AMC_CONTACT_OWNERSHIP";
DROP PACKAGE BODY "PQAMC"."AMC_COUNTRY";
DROP PACKAGE BODY "PQAMC"."AMC_EDUCATION";
DROP PACKAGE BODY "PQAMC"."AMC_GENDER";
DROP PACKAGE BODY "PQAMC"."AMC_OCCUPATION";
DROP PACKAGE BODY "PQAMC"."AMC_PROFESSION";
DROP PACKAGE BODY "PQAMC"."AMC_RESIDENTIAL_STATUS";
DROP PACKAGE BODY "PQAMC"."AMC_SOURCE_INCOME";
DROP PACKAGE BODY "PQAMC"."AMC_USER_REFRESH_TOKEN";
DROP PACKAGE BODY "PQAMC"."AMC_USER_SP";
--------------------------------------------------------
--  DDL for Table account_category
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_account_category" 
   (	"account_category_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table annual_income
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_annual_income" 
   (	"annual_income_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table area
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_area" 
   (	"area_id" NUMBER(10,0), 
	"city_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table bank
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_bank" 
   (	"bank_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table city
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_city" 
   (	"city_id" NUMBER(10,0), 
	"country_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table contact_ownership
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_contact_ownership" 
   (	"contact_ownership_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table country
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_country" 
   (	"country_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000), 
	"currency_name" NVARCHAR2(2000), 
	"currency_symbol" NVARCHAR2(2000), 
	"country_code" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table education
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_education" 
   (	"education_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table gender
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_gender" 
   (	"gender_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table occupation
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_occupation" 
   (	"occupation_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table profession
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_profession" 
   (	"profession_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table residential_status
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_residential_status" 
   (	"residential_status_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table source_income
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_source_income" 
   (	"source_income_id" NUMBER(10,0), 
	"name" NVARCHAR2(2000)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table user_refresh_token
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_user_refresh_token" 
   (	"user_refresh_token_id" NUMBER(10,0), 
	"user_id" NUMBER(10,0), 
	"refresh_token" NVARCHAR2(2000), 
	"token_date" TIMESTAMP (7)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table user_token
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_user_token" 
   (	"user_token_id" NUMBER(10,0), 
	"user_id" NUMBER(10,0), 
	"token" NVARCHAR2(2000), 
	"token_date" TIMESTAMP (7)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;
--------------------------------------------------------
--  DDL for Table user
--------------------------------------------------------

  CREATE TABLE "PQAMC"."amc_user" 
   (	"user_id" NUMBER(10,0), 
	"first_name" NVARCHAR2(2000), 
	"last_name" NVARCHAR2(2000), 
	"mobile_number" NVARCHAR2(2000), 
	"email" NVARCHAR2(2000), 
	"email_confirmed" NUMBER(1,0) DEFAULT 0, 
	"otp" NUMBER(10,0), 
	"profile_image" NVARCHAR2(2000), 
	"registration_date" TIMESTAMP (7)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "PQAMC" ;

--------------------------------------------------------
--  DDL for Package ACCOUNT_CATEGORY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_ACCOUNT_CATEGORY" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_account_category(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_account_categories(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_account_category(object_id NUMBER, record_count OUT NUMBER);
END AMC_account_category;

/
--------------------------------------------------------
--  DDL for Package ANNUAL_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_ANNUAL_INCOME" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_annual_income(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_annual_incomes(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_annual_income(object_id NUMBER, record_count OUT NUMBER);
END AMC_annual_income;

/
--------------------------------------------------------
--  DDL for Package AREA
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_AREA" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_area(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_areas(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_area(object_id NUMBER, record_count OUT NUMBER);
END AMC_area;

/
--------------------------------------------------------
--  DDL for Package BANK
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_BANK" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_bank(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_banks(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_bank(object_id NUMBER, record_count OUT NUMBER);
END AMC_bank;

/
--------------------------------------------------------
--  DDL for Package CITY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_CITY" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_city(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_cities(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_city(object_id NUMBER, record_count OUT NUMBER);
END AMC_city;

/
--------------------------------------------------------
--  DDL for Package CONTACT_OWNERSHIP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_CONTACT_OWNERSHIP" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_contact_ownership(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_contact_ownerships(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_contact_ownership(object_id NUMBER, record_count OUT NUMBER);
END AMC_contact_ownership;

/
--------------------------------------------------------
--  DDL for Package COUNTRY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_COUNTRY" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_country(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_countries(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_country(object_id NUMBER, record_count OUT NUMBER);
END AMC_country;

/
--------------------------------------------------------
--  DDL for Package EDUCATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_EDUCATION" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_education(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_educations(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_education(object_id NUMBER, record_count OUT NUMBER);
END AMC_education;

/
--------------------------------------------------------
--  DDL for Package GENDER
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_GENDER" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_gender(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_genders(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_gender(object_id NUMBER, record_count OUT NUMBER);
END AMC_gender;

/
--------------------------------------------------------
--  DDL for Package OCCUPATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_OCCUPATION" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_occupation(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_occupations(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_occupation(object_id NUMBER, record_count OUT NUMBER);
END AMC_occupation;

/
--------------------------------------------------------
--  DDL for Package PROFESSION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_PROFESSION" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_profession(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_professions(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_profession(object_id NUMBER, record_count OUT NUMBER);
END AMC_profession;

/
--------------------------------------------------------
--  DDL for Package RESIDENTIAL_STATUS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_RESIDENTIAL_STATUS" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_residential_status(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_residential_statuses(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_residential_status(object_id NUMBER, record_count OUT NUMBER);
END AMC_residential_status;

/
--------------------------------------------------------
--  DDL for Package SOURCE_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_SOURCE_INCOME" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_source_income(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_source_incomes(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_source_income(object_id NUMBER, record_count OUT NUMBER);
END AMC_source_income;

/
--------------------------------------------------------
--  DDL for Package USER_REFRESH_TOKEN
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_USER_REFRESH_TOKEN" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE GET_REFRESH_TOKEN(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE INSERT_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR);
    PROCEDURE INSERT_EXPIRE_TOKEN(UserId NUMBER, Token VARCHAR, TokenDate VARCHAR);
    PROCEDURE UPDATE_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR);
    PROCEDURE IS_EXPIRE_TOKEN(Token VARCHAR, recordset OUT NUMBER);
END AMC_USER_REFRESH_TOKEN;

/
--------------------------------------------------------
--  DDL for Package USER_SP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_USER_SP" AS 
  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE if_exist(mobile_number VARCHAR, email VARCHAR, recordset OUT NUMBER);
    PROCEDURE get_user(mobile_number VARCHAR, email VARCHAR, recordset OUT SYS_REFCURSOR);
    PROCEDURE get_users(record_set OUT SYS_REFCURSOR);
    PROCEDURE insert_user(email VARCHAR, mobile_number VARCHAR, OTP NUMBER, RegistrationDate VARCHAR, UsersList OUT SYS_REFCURSOR);
    PROCEDURE UPDATE_OTP(UserId NUMBER, OTP NUMBER);
END "AMC_USER_SP";

/
--------------------------------------------------------
--  DDL for Package ACCOUNT_CATEGORY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_ACCOUNT_CATEGORY" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_account_category(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_account_categories(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_account_category(object_id NUMBER, record_count OUT NUMBER);
END AMC_account_category;

/
--------------------------------------------------------
--  DDL for Package ANNUAL_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_ANNUAL_INCOME" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_annual_income(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_annual_incomes(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_annual_income(object_id NUMBER, record_count OUT NUMBER);
END AMC_annual_income;

/
--------------------------------------------------------
--  DDL for Package AREA
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_AREA" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_area(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_areas(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_area(object_id NUMBER, record_count OUT NUMBER);
END AMC_area;

/
--------------------------------------------------------
--  DDL for Package BANK
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_BANK" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_bank(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_banks(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_bank(object_id NUMBER, record_count OUT NUMBER);
END AMC_bank;

/
--------------------------------------------------------
--  DDL for Package CITY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_CITY" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_city(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_cities(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_city(object_id NUMBER, record_count OUT NUMBER);
END AMC_city;

/
--------------------------------------------------------
--  DDL for Package CONTACT_OWNERSHIP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_CONTACT_OWNERSHIP" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_contact_ownership(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_contact_ownerships(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_contact_ownership(object_id NUMBER, record_count OUT NUMBER);
END AMC_contact_ownership;

/
--------------------------------------------------------
--  DDL for Package COUNTRY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_COUNTRY" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_country(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_countries(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_country(object_id NUMBER, record_count OUT NUMBER);
END AMC_country;

/
--------------------------------------------------------
--  DDL for Package EDUCATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_EDUCATION" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_education(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_educations(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_education(object_id NUMBER, record_count OUT NUMBER);
END AMC_education;

/
--------------------------------------------------------
--  DDL for Package GENDER
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_GENDER" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_gender(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_genders(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_gender(object_id NUMBER, record_count OUT NUMBER);
END AMC_gender;

/
--------------------------------------------------------
--  DDL for Package OCCUPATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_OCCUPATION" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_occupation(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_occupations(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_occupation(object_id NUMBER, record_count OUT NUMBER);
END AMC_occupation;

/
--------------------------------------------------------
--  DDL for Package PROFESSION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_PROFESSION" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_profession(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_professions(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_profession(object_id NUMBER, record_count OUT NUMBER);
END AMC_profession;

/
--------------------------------------------------------
--  DDL for Package RESIDENTIAL_STATUS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_RESIDENTIAL_STATUS" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_residential_status(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_residential_statuses(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_residential_status(object_id NUMBER, record_count OUT NUMBER);
END AMC_residential_status;

/
--------------------------------------------------------
--  DDL for Package SOURCE_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_SOURCE_INCOME" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_source_income(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_source_incomes(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_source_income(object_id NUMBER, record_count OUT NUMBER);
END AMC_source_income;

/
--------------------------------------------------------
--  DDL for Package USER_REFRESH_TOKEN
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_USER_REFRESH_TOKEN" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE GET_REFRESH_TOKEN(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE INSERT_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR);
    PROCEDURE INSERT_EXPIRE_TOKEN(UserId NUMBER, Token VARCHAR, TokenDate VARCHAR);
    PROCEDURE UPDATE_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR);
    PROCEDURE IS_EXPIRE_TOKEN(Token VARCHAR, recordset OUT NUMBER);
END AMC_USER_REFRESH_TOKEN;

/
--------------------------------------------------------
--  DDL for Package USER_SP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_USER_SP" AS 
  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE if_exist(mobile_number VARCHAR, email VARCHAR, recordset OUT NUMBER);
    PROCEDURE get_user(mobile_number VARCHAR, email VARCHAR, recordset OUT SYS_REFCURSOR);
    PROCEDURE get_users(record_set OUT SYS_REFCURSOR);
    PROCEDURE insert_user(email VARCHAR, mobile_number VARCHAR, OTP NUMBER, RegistrationDate VARCHAR, UsersList OUT SYS_REFCURSOR);
    PROCEDURE UPDATE_OTP(UserId NUMBER, OTP NUMBER);
END "AMC_USER_SP";

/
--------------------------------------------------------
--  DDL for Package Body ACCOUNT_CATEGORY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_ACCOUNT_CATEGORY" AS

  PROCEDURE get_account_category(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE account_category.get_account_category
    OPEN record_set FOR
        SELECT * from "amc_account_category" where "account_category_id" = object_id;
  END get_account_category;

  PROCEDURE get_account_categories(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE account_category.get_account_categories
    OPEN record_set FOR
        SELECT * from "amc_account_category";
  END get_account_categories;

  PROCEDURE del_account_category(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE account_category.get_account_category
    DELETE from "amc_account_category" where "account_category_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_account_category;

END AMC_account_category;

/
--------------------------------------------------------
--  DDL for Package Body ANNUAL_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_ANNUAL_INCOME" AS

  PROCEDURE get_annual_income(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    OPEN record_set FOR
        SELECT * from "amc_annual_income" where "annual_income_id" = object_id;
  END get_annual_income;

  PROCEDURE get_annual_incomes(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_genders
    OPEN record_set FOR
        SELECT * from "amc_annual_income";
  END get_annual_incomes;

  PROCEDURE del_annual_income(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    DELETE from "amc_annual_income" where "annual_income_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_annual_income;

END "AMC_ANNUAL_INCOME";

/
--------------------------------------------------------
--  DDL for Package Body AREA
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_AREA" AS

  PROCEDURE get_area(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_area
    OPEN record_set FOR
        SELECT * from "amc_area" where "area_id" = object_id;
  END get_area;

  PROCEDURE get_areas(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_areas
    OPEN record_set FOR
        SELECT * from "amc_area";
  END get_areas;

  PROCEDURE del_area(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_area
    DELETE from "amc_area" where "area_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_area;

END "AMC_AREA";

/
--------------------------------------------------------
--  DDL for Package Body BANK
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_BANK" AS

  PROCEDURE get_bank(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE bank.get_bank
    OPEN record_set FOR
        SELECT * from "amc_bank" where "bank_id" = object_id;
  END get_bank;

  PROCEDURE get_banks(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE bank.get_banks
    OPEN record_set FOR
        SELECT * from "amc_bank";
  END get_banks;

  PROCEDURE del_bank(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE bank.get_bank
    DELETE from "amc_bank" where "bank_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_bank;

END "AMC_BANK";

/
--------------------------------------------------------
--  DDL for Package Body CITY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_CITY" AS

  PROCEDURE get_city(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_city
    OPEN record_set FOR
        SELECT * from "amc_city" where "city_id" = object_id;
  END get_city;

  PROCEDURE get_cities(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_cities
    OPEN record_set FOR
        SELECT * from "amc_city";
  END get_cities;
  
  PROCEDURE del_city(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_city
    DELETE from "amc_city" where "city_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_city;

END "AMC_CITY";

/
--------------------------------------------------------
--  DDL for Package Body CONTACT_OWNERSHIP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_CONTACT_OWNERSHIP" AS

  PROCEDURE get_contact_ownership(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE contact_ownership.get_contact_ownership
    OPEN record_set FOR
        SELECT * from "amc_contact_ownership" where "contact_ownership_id" = object_id;
  END get_contact_ownership;

  PROCEDURE get_contact_ownerships(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE contact_ownership.get_contact_ownerships
    OPEN record_set FOR
        SELECT * from "amc_contact_ownership";
  END get_contact_ownerships;

  PROCEDURE del_contact_ownership(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE contact_ownership.get_contact_ownership
    DELETE from "amc_contact_ownership" where "contact_ownership_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_contact_ownership;

END "AMC_CONTACT_OWNERSHIP";

/
--------------------------------------------------------
--  DDL for Package Body COUNTRY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_COUNTRY" AS

  PROCEDURE get_country(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE COUNTRY.get_country
    OPEN record_set FOR
        SELECT * from "amc_country" where "country_id" = object_id;
  END get_country;

  PROCEDURE get_countries(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE COUNTRY.get_countries
    OPEN record_set FOR
        SELECT * from "amc_country";
  END get_countries;
    
  PROCEDURE del_country(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_city
    DELETE from "amc_country" where "country_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_country;


END "AMC_COUNTRY";

/
--------------------------------------------------------
--  DDL for Package Body EDUCATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_EDUCATION" AS

  PROCEDURE get_education(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE education.get_education
    OPEN record_set FOR
        SELECT * from "amc_education" where "education_id" = object_id;
  END get_education;

  PROCEDURE get_educations(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE education.get_educations
    OPEN record_set FOR
        SELECT * from "amc_education";
  END get_educations;

  PROCEDURE del_education(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE education.get_education
    DELETE from "amc_education" where "education_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_education;

END "AMC_EDUCATION";

/
--------------------------------------------------------
--  DDL for Package Body GENDER
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_GENDER" AS

  PROCEDURE get_gender(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    OPEN record_set FOR
        SELECT * from "amc_gender" where "gender_id" = object_id;
  END get_gender;

  PROCEDURE get_genders(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_genders
    OPEN record_set FOR
        SELECT * from "amc_gender";
  END get_genders;

  PROCEDURE del_gender(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    DELETE from "amc_gender" where "gender_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_gender;

END "AMC_GENDER";

/
--------------------------------------------------------
--  DDL for Package Body OCCUPATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_OCCUPATION" AS

  PROCEDURE get_occupation(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE occupation.get_occupation
    OPEN record_set FOR
        SELECT * from "amc_occupation" where "occupation_id" = object_id;
  END get_occupation;

  PROCEDURE get_occupations(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE occupation.get_occupations
    OPEN record_set FOR
        SELECT * from "amc_occupation";
  END get_occupations;

  PROCEDURE del_occupation(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE occupation.get_occupation
    DELETE from "amc_occupation" where "occupation_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_occupation;

END "AMC_OCCUPATION";

/
--------------------------------------------------------
--  DDL for Package Body PROFESSION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_PROFESSION" AS

  PROCEDURE get_profession(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE profession.get_profession
    OPEN record_set FOR
        SELECT * from "amc_profession" where "profession_id" = object_id;
  END get_profession;

  PROCEDURE get_professions(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE profession.get_professions
    OPEN record_set FOR
        SELECT * from "amc_profession";
  END get_professions;

  PROCEDURE del_profession(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE profession.get_profession
    DELETE from "amc_profession" where "profession_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_profession;

END "AMC_PROFESSION";

/
--------------------------------------------------------
--  DDL for Package Body RESIDENTIAL_STATUS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_RESIDENTIAL_STATUS" AS

  PROCEDURE get_residential_status(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    OPEN record_set FOR
        SELECT * from "amc_residential_status" where "residential_status_id" = object_id;
  END get_residential_status;

  PROCEDURE get_residential_statuses(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_genders
    OPEN record_set FOR
        SELECT * from "amc_residential_status";
  END get_residential_statuses;

  PROCEDURE del_residential_status(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    DELETE from "amc_residential_status" where "residential_status_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_residential_status;

END "AMC_RESIDENTIAL_STATUS";

/
--------------------------------------------------------
--  DDL for Package Body SOURCE_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_SOURCE_INCOME" AS

  PROCEDURE get_source_income(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE source_income.get_source_income
    OPEN record_set FOR
        SELECT * from "amc_source_income" where "source_income_id" = object_id;
  END get_source_income;

  PROCEDURE get_source_incomes(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE source_income.get_source_incomes
    OPEN record_set FOR
        SELECT * from "amc_source_income";
  END get_source_incomes;

  PROCEDURE del_source_income(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE source_income.get_source_income
    DELETE from "amc_source_income" where "source_income_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_source_income;

END "AMC_SOURCE_INCOME";

/
--------------------------------------------------------
--  DDL for Package Body USER_REFRESH_TOKEN
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_USER_REFRESH_TOKEN" AS

  PROCEDURE GET_REFRESH_TOKEN(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_area
    OPEN record_set FOR
        SELECT * from "amc_user_refresh_token" where "user_id" = object_id;
  END GET_REFRESH_TOKEN;
  
  PROCEDURE INSERT_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR) AS
  BEGIN
    INSERT INTO "amc_user_refresh_token"("user_id", "refresh_token", "token_date" ) values(UserId, RefreshToken, TO_DATE(TokenDate, 'yyyy-MM-dd HH24:MI:ss'));
    OPEN UserToken FOR
        SELECT * from "amc_user_refresh_token" where "user_id" = UserId;
  END INSERT_REFRESH_TOKEN;
    
  PROCEDURE UPDATE_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR) AS
  BEGIN
    UPDATE "amc_user_refresh_token" SET "refresh_token" = RefreshToken, "token_date" = TO_DATE(TokenDate, 'yyyy-MM-dd HH24:MI:ss') WHERE "user_id" = UserId;
    OPEN UserToken FOR
        SELECT * from "amc_user_refresh_token" where "user_id" = UserId;
  END UPDATE_REFRESH_TOKEN;
  
  PROCEDURE INSERT_EXPIRE_TOKEN(UserId NUMBER, Token VARCHAR, TokenDate VARCHAR) AS
  BEGIN
    INSERT INTO "amc_user_token"("user_id", "token", "token_date" ) values(UserId, Token, TO_DATE(TokenDate, 'yyyy-MM-dd HH24:MI:ss'));
  END INSERT_EXPIRE_TOKEN;
  
  
  PROCEDURE IS_EXPIRE_TOKEN(Token VARCHAR, recordset OUT NUMBER) AS
  user_count NUMBER;
  BEGIN
    SELECT count(*)  INTO user_count from "amc_user_token" where "token" = Token;
    recordset := user_count;
  END IS_EXPIRE_TOKEN;


END "AMC_USER_REFRESH_TOKEN";

/
--------------------------------------------------------
--  DDL for Package Body USER_SP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_USER_SP" AS

  PROCEDURE if_exist(mobile_number VARCHAR, email VARCHAR, recordset OUT NUMBER) AS
  user_count NUMBER;
  BEGIN
    SELECT count(*)  INTO user_count from "amc_user" where "mobile_number" = mobile_number or "email" = email;
    recordset := user_count;
  END if_exist;
  
  PROCEDURE get_user(mobile_number VARCHAR, email VARCHAR, recordset OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN recordset FOR
        SELECT * from "amc_user" where "mobile_number" = mobile_number or "email" = email;
  END get_user;

  PROCEDURE insert_user(email VARCHAR, mobile_number VARCHAR, OTP NUMBER, RegistrationDate VARCHAR, UsersList OUT SYS_REFCURSOR) AS
  BEGIN
    INSERT INTO "amc_user"("mobile_number", "email", "otp", "registration_date" ) values(mobile_number, email, OTP, TO_DATE(RegistrationDate, 'yyyy-MM-dd HH24:MI:ss'));
    OPEN UsersList FOR
        SELECT "user_id", "email", "mobile_number",  "otp", "registration_date"  from "amc_user" where "mobile_number" = mobile_number;
  END insert_user;
  
  
  PROCEDURE get_users(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_cities
    OPEN record_set FOR
        SELECT * from "amc_user";
  END get_users;
  
   PROCEDURE UPDATE_OTP(UserId NUMBER, OTP NUMBER) AS
   BEGIN
    UPDATE "amc_user" SET "otp" = OTP WHERE "user_id" = UserId;
  END UPDATE_OTP;

END "AMC_USER_SP";

/
