--------------------------------------------------------
--  DDL for Package ACCOUNT_CATEGORY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE AMC_ACCOUNT_CATEGORY_PKG AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_account_category(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_account_categories(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_account_category(object_id NUMBER, record_count OUT NUMBER);
END AMC_ACCOUNT_CATEGORY_PKG;

/
--------------------------------------------------------
--  DDL for Package ANNUAL_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_ANNUAL_INCOME_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_annual_income(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_annual_incomes(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_annual_income(object_id NUMBER, record_count OUT NUMBER);
END AMC_ANNUAL_INCOME_PKG;

/
--------------------------------------------------------
--  DDL for Package AREA
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_AREA_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_area(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_areas(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_area(object_id NUMBER, record_count OUT NUMBER);
END AMC_AREA_PKG;

/
--------------------------------------------------------
--  DDL for Package BANK
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_BANK_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_bank(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_banks(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_bank(object_id NUMBER, record_count OUT NUMBER);
END AMC_BANK_PKG;

/
--------------------------------------------------------
--  DDL for Package CITY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_CITY_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_city(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_cities(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_city(object_id NUMBER, record_count OUT NUMBER);
END AMC_CITY_PKG;

/
--------------------------------------------------------
--  DDL for Package CONTACT_OWNERSHIP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_CONTACT_OWNERSHIP_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_contact_ownership(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_contact_ownerships(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_contact_ownership(object_id NUMBER, record_count OUT NUMBER);
END AMC_CONTACT_OWNERSHIP_PKG;

/
--------------------------------------------------------
--  DDL for Package COUNTRY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_COUNTRY_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_country(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_countries(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_country(object_id NUMBER, record_count OUT NUMBER);
END AMC_COUNTRY_PKG;

/
--------------------------------------------------------
--  DDL for Package EDUCATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_EDUCATION_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_education(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_educations(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_education(object_id NUMBER, record_count OUT NUMBER);
END AMC_EDUCATION_PKG;

/
--------------------------------------------------------
--  DDL for Package GENDER
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_GENDER_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_gender(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_genders(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_gender(object_id NUMBER, record_count OUT NUMBER);
END AMC_GENDER_PKG;

/
--------------------------------------------------------
--  DDL for Package OCCUPATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_OCCUPATION_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_occupation(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_occupations(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_occupation(object_id NUMBER, record_count OUT NUMBER);
END AMC_OCCUPATION_PKG;

/
--------------------------------------------------------
--  DDL for Package PROFESSION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_PROFESSION_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_profession(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_professions(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_profession(object_id NUMBER, record_count OUT NUMBER);
END AMC_PROFESSION_PKG;

/
--------------------------------------------------------
--  DDL for Package RESIDENTIAL_STATUS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_RESIDENTIAL_STATUS_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_residential_status(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_residential_statuses(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_residential_status(object_id NUMBER, record_count OUT NUMBER);
END AMC_RESIDENTIAL_STATUS_PKG;

/
--------------------------------------------------------
--  DDL for Package INCOME_SOURCE
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_INCOME_SOURCE_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE get_source_income(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE get_source_incomes(record_set OUT SYS_REFCURSOR);
    PROCEDURE del_source_income(object_id NUMBER, record_count OUT NUMBER);
END AMC_INCOME_SOURCE_PKG;

/
--------------------------------------------------------
--  DDL for Package USER_REFRESH_TOKEN
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_USER_REFRESH_TOKEN_PKG" AS 

  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE GET_REFRESH_TOKEN(object_id NUMBER, record_set OUT SYS_REFCURSOR);
    PROCEDURE INSERT_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR);
    PROCEDURE INSERT_EXPIRE_TOKEN(UserId NUMBER, Token VARCHAR, TokenDate VARCHAR);
    PROCEDURE UPDATE_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR);
    PROCEDURE IS_EXPIRE_TOKEN(Token VARCHAR, recordset OUT NUMBER);
END AMC_USER_REFRESH_TOKEN_PKG;

/
--------------------------------------------------------
--  DDL for Package USER_SP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE "AMC_USER_SP_PKG" AS 
  /* TODO enter package declarations (types, exceptions, methods etc) here */ 
    PROCEDURE if_exist(mobile_number VARCHAR, email VARCHAR, recordset OUT NUMBER);
    PROCEDURE get_user(mobile_number VARCHAR, email VARCHAR, recordset OUT SYS_REFCURSOR);
    PROCEDURE get_users(record_set OUT SYS_REFCURSOR);
    PROCEDURE insert_user(email VARCHAR, mobile_number VARCHAR, OTP NUMBER, RegistrationDate VARCHAR, UsersList OUT SYS_REFCURSOR);
    PROCEDURE UPDATE_OTP(UserId NUMBER, OTP NUMBER);
END "AMC_USER_SP_PKG";

/
--------------------------------------------------------
--  DDL for Package Body ACCOUNT_CATEGORY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_ACCOUNT_CATEGORY_PKG" AS

  PROCEDURE get_account_category(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE account_category.get_account_category
    OPEN record_set FOR
        SELECT * from "AMC_ACCOUNT_CATEGORY" where "account_category_id" = object_id;
  END get_account_category;

  PROCEDURE get_account_categories(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE account_category.get_account_categories
    OPEN record_set FOR
        SELECT * from "AMC_ACCOUNT_CATEGORY";
  END get_account_categories;

  PROCEDURE del_account_category(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE account_category.get_account_category
    DELETE from "AMC_ACCOUNT_CATEGORY" where "account_category_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_account_category;

END AMC_ACCOUNT_CATEGORY_PKG;

/
--------------------------------------------------------
--  DDL for Package Body ANNUAL_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_ANNUAL_INCOME_PKG" AS

  PROCEDURE get_annual_income(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    OPEN record_set FOR
        SELECT * from "AMC_ANNUAL_INCOME" where "annual_income_id" = object_id;
  END get_annual_income;

  PROCEDURE get_annual_incomes(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_genders
    OPEN record_set FOR
        SELECT * from "AMC_ANNUAL_INCOME";
  END get_annual_incomes;

  PROCEDURE del_annual_income(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    DELETE from "AMC_ANNUAL_INCOME" where "annual_income_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_annual_income;

END "AMC_ANNUAL_INCOME_PKG";

/
--------------------------------------------------------
--  DDL for Package Body AREA
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_AREA_PKG" AS

  PROCEDURE get_area(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_area
    OPEN record_set FOR
        SELECT * from "AMC_AREA" where "area_id" = object_id;
  END get_area;

  PROCEDURE get_areas(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_areas
    OPEN record_set FOR
        SELECT * from "AMC_AREA";
  END get_areas;

  PROCEDURE del_area(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_area
    DELETE from "AMC_AREA" where "area_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_area;

END "AMC_AREA_PKG";

/
--------------------------------------------------------
--  DDL for Package Body BANK
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_BANK_PKG" AS

  PROCEDURE get_bank(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE bank.get_bank
    OPEN record_set FOR
        SELECT * from "AMC_BANK" where "bank_id" = object_id;
  END get_bank;

  PROCEDURE get_banks(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE bank.get_banks
    OPEN record_set FOR
        SELECT * from "AMC_BANK";
  END get_banks;

  PROCEDURE del_bank(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE bank.get_bank
    DELETE from "AMC_BANK" where "bank_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_bank;

END "AMC_BANK_PKG";

/
--------------------------------------------------------
--  DDL for Package Body CITY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_CITY_PKG" AS

  PROCEDURE get_city(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_city
    OPEN record_set FOR
        SELECT * from "AMC_CITY" where "city_id" = object_id;
  END get_city;

  PROCEDURE get_cities(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_cities
    OPEN record_set FOR
        SELECT * from "AMC_CITY";
  END get_cities;
  
  PROCEDURE del_city(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_city
    DELETE from "AMC_CITY" where "city_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_city;

END "AMC_CITY_PKG";

/
--------------------------------------------------------
--  DDL for Package Body CONTACT_OWNERSHIP
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_CONTACT_OWNERSHIP_PKG" AS

  PROCEDURE get_contact_ownership(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE contact_ownership.get_contact_ownership
    OPEN record_set FOR
        SELECT * from "AMC_CONTACT_OWNERSHIP" where "contact_ownership_id" = object_id;
  END get_contact_ownership;

  PROCEDURE get_contact_ownerships(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE contact_ownership.get_contact_ownerships
    OPEN record_set FOR
        SELECT * from "AMC_CONTACT_OWNERSHIP";
  END get_contact_ownerships;

  PROCEDURE del_contact_ownership(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE contact_ownership.get_contact_ownership
    DELETE from "AMC_CONTACT_OWNERSHIP" where "contact_ownership_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_contact_ownership;

END "AMC_CONTACT_OWNERSHIP_PKG";

/
--------------------------------------------------------
--  DDL for Package Body COUNTRY
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_COUNTRY_PKG" AS

  PROCEDURE get_country(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE COUNTRY.get_country
    OPEN record_set FOR
        SELECT * from "AMC_COUNTRY" where "country_id" = object_id;
  END get_country;

  PROCEDURE get_countries(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE COUNTRY.get_countries
    OPEN record_set FOR
        SELECT * from "AMC_COUNTRY";
  END get_countries;
    
  PROCEDURE del_country(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_city
    DELETE from "AMC_COUNTRY" where "country_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_country;


END "AMC_COUNTRY_PKG";

/
--------------------------------------------------------
--  DDL for Package Body EDUCATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_EDUCATION_PKG" AS

  PROCEDURE get_education(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE education.get_education
    OPEN record_set FOR
        SELECT * from "AMC_EDUCATION" where "education_id" = object_id;
  END get_education;

  PROCEDURE get_educations(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE education.get_educations
    OPEN record_set FOR
        SELECT * from "AMC_EDUCATION";
  END get_educations;

  PROCEDURE del_education(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE education.get_education
    DELETE from "AMC_EDUCATION" where "education_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_education;

END "AMC_EDUCATION_PKG";

/
--------------------------------------------------------
--  DDL for Package Body GENDER
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_GENDER_PKG" AS

  PROCEDURE get_gender(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    OPEN record_set FOR
        SELECT * from "AMC_GENDER" where "gender_id" = object_id;
  END get_gender;

  PROCEDURE get_genders(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_genders
    OPEN record_set FOR
        SELECT * from "AMC_GENDER";
  END get_genders;

  PROCEDURE del_gender(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    DELETE from "AMC_GENDER" where "gender_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_gender;

END "AMC_GENDER_PKG";

/
--------------------------------------------------------
--  DDL for Package Body OCCUPATION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_OCCUPATION_PKG" AS

  PROCEDURE get_occupation(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE occupation.get_occupation
    OPEN record_set FOR
        SELECT * from "AMC_OCCUPATION" where "occupation_id" = object_id;
  END get_occupation;

  PROCEDURE get_occupations(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE occupation.get_occupations
    OPEN record_set FOR
        SELECT * from "AMC_OCCUPATION";
  END get_occupations;

  PROCEDURE del_occupation(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE occupation.get_occupation
    DELETE from "AMC_OCCUPATION" where "occupation_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_occupation;

END "AMC_OCCUPATION_PKG";

/
--------------------------------------------------------
--  DDL for Package Body PROFESSION
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_PROFESSION_PKG" AS

  PROCEDURE get_profession(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE profession.get_profession
    OPEN record_set FOR
        SELECT * from "AMC_PROFESSION" where "profession_id" = object_id;
  END get_profession;

  PROCEDURE get_professions(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE profession.get_professions
    OPEN record_set FOR
        SELECT * from "AMC_PROFESSION";
  END get_professions;

  PROCEDURE del_profession(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE profession.get_profession
    DELETE from "AMC_PROFESSION" where "profession_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_profession;

END "AMC_PROFESSION_PKG";

/
--------------------------------------------------------
--  DDL for Package Body RESIDENTIAL_STATUS
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_RESIDENTIAL_STATUS_PKG" AS

  PROCEDURE get_residential_status(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    OPEN record_set FOR
        SELECT * from "AMC_RESIDENTIAL_STATUS" where "residential_status_id" = object_id;
  END get_residential_status;

  PROCEDURE get_residential_statuses(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_genders
    OPEN record_set FOR
        SELECT * from "AMC_RESIDENTIAL_STATUS";
  END get_residential_statuses;

  PROCEDURE del_residential_status(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE gender.get_gender
    DELETE from "AMC_RESIDENTIAL_STATUS" where "residential_status_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_residential_status;

END "AMC_RESIDENTIAL_STATUS_PKG";

/
--------------------------------------------------------
--  DDL for Package Body SOURCE_INCOME
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_INCOME_SOURCE_PKG" AS

  PROCEDURE get_source_income(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE source_income.get_source_income
    OPEN record_set FOR
        SELECT * from "AMC_INCOME_SOURCE" where "income_source_id" = object_id;
  END get_source_income;

  PROCEDURE get_source_incomes(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE source_income.get_source_incomes
    OPEN record_set FOR
        SELECT * from "AMC_INCOME_SOURCE";
  END get_source_incomes;

  PROCEDURE del_source_income(object_id NUMBER, record_count OUT Number) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE source_income.get_source_income
    DELETE from "AMC_INCOME_SOURCE" where "income_source_id" = object_id ;
    record_count := SQL%ROWCOUNT;
  END del_source_income;

END "AMC_INCOME_SOURCE_PKG";

/
--------------------------------------------------------
--  DDL for Package Body USER_REFRESH_TOKEN
--------------------------------------------------------

  CREATE OR REPLACE PACKAGE BODY "AMC_USER_REFRESH_TOKEN_PKG" AS

  PROCEDURE GET_REFRESH_TOKEN(object_id NUMBER, record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE area.get_area
    OPEN record_set FOR
        SELECT * from "AMC_USER_REFRESH_TOKEN" where "user_id" = object_id;
  END GET_REFRESH_TOKEN;
  
  PROCEDURE INSERT_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR) AS
  BEGIN
    INSERT INTO "AMC_USER_REFRESH_TOKEN"("user_id", "refresh_token", "token_date" ) values(UserId, RefreshToken, TO_DATE(TokenDate, 'yyyy-MM-dd HH24:MI:ss'));
    OPEN UserToken FOR
        SELECT * from "AMC_USER_REFRESH_TOKEN" where "user_id" = UserId;
  END INSERT_REFRESH_TOKEN;
    
  PROCEDURE UPDATE_REFRESH_TOKEN(UserId NUMBER, RefreshToken VARCHAR, TokenDate VARCHAR, UserToken OUT SYS_REFCURSOR) AS
  BEGIN
    UPDATE "AMC_USER_REFRESH_TOKEN" SET "refresh_token" = RefreshToken, "token_date" = TO_DATE(TokenDate, 'yyyy-MM-dd HH24:MI:ss') WHERE "user_id" = UserId;
    OPEN UserToken FOR
        SELECT * from "AMC_USER_REFRESH_TOKEN" where "user_id" = UserId;
  END UPDATE_REFRESH_TOKEN;
  
  PROCEDURE INSERT_EXPIRE_TOKEN(UserId NUMBER, Token VARCHAR, TokenDate VARCHAR) AS
  BEGIN
    INSERT INTO "AMC_USER_TOKEN"("user_id", "token", "token_date" ) values(UserId, Token, TO_DATE(TokenDate, 'yyyy-MM-dd HH24:MI:ss'));
  END INSERT_EXPIRE_TOKEN;
  
  
  PROCEDURE IS_EXPIRE_TOKEN(Token VARCHAR, recordset OUT NUMBER) AS
  user_count NUMBER;
  BEGIN
    SELECT count(*)  INTO user_count from "AMC_USER_TOKEN" where "token" = Token;
    recordset := user_count;
  END IS_EXPIRE_TOKEN;


END "AMC_USER_REFRESH_TOKEN_PKG";

/
--------------------------------------------------------
--  DDL for Package Body USER_SP
--------------------------------------------------------

create or replace NONEDITIONABLE PACKAGE BODY "AMC_USER_SP_PKG" AS

  PROCEDURE if_exist(mobile_number VARCHAR, email VARCHAR, recordset OUT NUMBER) AS
  user_count NUMBER;
  BEGIN
    SELECT count(*)  INTO user_count from "AMC_USER" where "mobile_number" = mobile_number or "email" = email;
    recordset := user_count;
  END if_exist;

  PROCEDURE get_user(mobile_number VARCHAR, email VARCHAR, recordset OUT SYS_REFCURSOR) AS
  BEGIN
    OPEN recordset FOR
        SELECT * from "AMC_USER" where "mobile_number" = mobile_number or "email" = email;
  END get_user;

  PROCEDURE insert_user(email VARCHAR, mobile_number VARCHAR, OTP NUMBER, RegistrationDate VARCHAR, UsersList OUT SYS_REFCURSOR) AS
  BEGIN
    INSERT INTO "AMC_USER"("mobile_number", "email", "otp", "registration_date" ) values(mobile_number, email, OTP, TO_DATE(RegistrationDate, 'yyyy-MM-dd HH24:MI:ss'));
    OPEN UsersList FOR
        SELECT "user_id", "email", "mobile_number",  "otp", "registration_date"  from "AMC_USER" where "mobile_number" = mobile_number;
  END insert_user;


  PROCEDURE get_users(record_set OUT SYS_REFCURSOR) AS
  BEGIN
    -- TODO: Implementation required for PROCEDURE CITY.get_cities
    OPEN record_set FOR
        SELECT * from "AMC_USER";
  END get_users;

   PROCEDURE UPDATE_OTP(UserId NUMBER, OTP NUMBER) AS
   BEGIN
    UPDATE "AMC_USER" SET "otp" = OTP WHERE "user_id" = UserId;
  END UPDATE_OTP;

END "AMC_USER_SP_PKG";

/

CREATE SEQUENCE  "AMC_ACCOUNT_CATEGORY_ID_SEQ";
CREATE SEQUENCE  "AMC_ANNUAL_INCOME_ID_SEQ";  
CREATE SEQUENCE  "AMC_AREA_ID_SEQ";  
CREATE SEQUENCE  "AMC_BANK_ID_SEQ";  
CREATE SEQUENCE  "AMC_CITY_ID_SEQ";  
CREATE SEQUENCE  "AMC_CONTACT_OWNERSHIP_ID_SEQ";  
CREATE SEQUENCE  "AMC_COUNTRY_ID_SEQ";  
CREATE SEQUENCE  "AMC_EDUCATION_ID_SEQ";  
CREATE SEQUENCE  "AMC_GENER_ID_SEQ";  
CREATE SEQUENCE  "AMC_OCCUPATION_ID_SEQ";  
CREATE SEQUENCE  "AMC_PROFESSION_ID_SEQ";  
CREATE SEQUENCE  "AMC_RESIDENTIAL_STATUS_ID_SEQ";  
CREATE SEQUENCE  "AMC_RESIDENTIAL_STATUS_ID_SEQ";  
CREATE SEQUENCE  "AMC_INCOME_SOURCE_ID_SEQ";  
CREATE SEQUENCE  "AMC_USE_ID_SEQ";  
CREATE SEQUENCE  "AMC_USE_REFRESH_TOKEN_SEQ";  
CREATE SEQUENCE  "AMC_USE_TOKEN_SEQ";  
/
