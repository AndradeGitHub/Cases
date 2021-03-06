DECLARE

   ID_PROF NUMBER;
   ID_PROF_APROV NUMBER;   
   ID_PROF_OPER NUMBER;   
   ID_PROF_CONS NUMBER;   
   
   ID_US NUMBER;
   ID_USER_APROV NUMBER;   
   ID_USER_OPER NUMBER;   
   ID_USER_CONS NUMBER;   

BEGIN

/******************************************** MODULE ********************************************/
  INSERT INTO TSYS_MODULE (ID_MODULE, DS_NAME) VALUES ('QUALITYCONTROL', 'QUALITY CONTROL');    

/******************************************** PROFILE ********************************************/
  SELECT ID_PROFILE INTO ID_PROF FROM TSYS_PROFILE WHERE ROWNUM = 1 ORDER BY ID_PROFILE DESC;

  ID_PROF_APROV := ID_PROF + 1;   
  ID_PROF_OPER  := ID_PROF + 2;   
  ID_PROF_CONS  := ID_PROF + 3;   
  
  INSERT INTO TSYS_PROFILE (ID_PROFILE, DS_NAME, IN_INACTIVE) VALUES (ID_PROF_APROV, 'QC_APROVADOR', 'N');        
  INSERT INTO TSYS_PROFILE (ID_PROFILE, DS_NAME, IN_INACTIVE) VALUES (ID_PROF_OPER, 'QC_OPERACIONAL', 'N');   
  INSERT INTO TSYS_PROFILE (ID_PROFILE, DS_NAME, IN_INACTIVE) VALUES (ID_PROF_CONS, 'QC_CONSULTA', 'N');     
  
/******************************************** USER ********************************************/
  SELECT ID_USER INTO ID_US FROM TSYS_USER WHERE ROWNUM = 1 ORDER BY ID_USER DESC;  
  
  ID_USER_APROV := ID_US + 1;   
  ID_USER_OPER  := ID_US + 2;   
  ID_USER_CONS  := ID_US + 3;         
  
  INSERT INTO TSYS_USER (ID_USER, DS_NAME, DS_LOGIN, DS_PASSWORD, NR_LOGON_ATTEMPT, DT_PASSWORD, DT_UPDATE, IN_INACTIVE, IN_CHANGE_PASSWORD) 
                 VALUES (ID_USER_APROV, 'QC_APROVADOR_USER', 'QC_APROVADOR_USER', '152 159 89 85 26 36 105 97 253 129 106 126 34 219 235 138 172 209 222 17', 0, '10/10/13', '10/10/13', 'N', 'N');     
  INSERT INTO TSYS_USER (ID_USER, DS_NAME, DS_LOGIN, DS_PASSWORD, NR_LOGON_ATTEMPT, DT_PASSWORD, DT_UPDATE, IN_INACTIVE, IN_CHANGE_PASSWORD) 
                 VALUES (ID_USER_OPER, 'QC_OPERACIONAL_USER', 'QC_OPERACIONAL_USER', '152 159 89 85 26 36 105 97 253 129 106 126 34 219 235 138 172 209 222 17', 0, '10/10/2013', '10/10/2013', 'N', 'N');     
  INSERT INTO TSYS_USER (ID_USER, DS_NAME, DS_LOGIN, DS_PASSWORD, NR_LOGON_ATTEMPT, DT_PASSWORD, DT_UPDATE, IN_INACTIVE, IN_CHANGE_PASSWORD) 
                 VALUES (ID_USER_CONS, 'QC_CONSULTA_USER', 'QC_CONSULTA_USER', '152 159 89 85 26 36 105 97 253 129 106 126 34 219 235 138 172 209 222 17', 0, '10/10/2013', '10/10/2013', 'N', 'N');     

/******************************************** USER PROFILE ********************************************/
  INSERT INTO TSYS_USER_PROFILE VALUES (ID_USER_APROV, ID_PROF_APROV);
  INSERT INTO TSYS_USER_PROFILE VALUES (ID_USER_OPER, ID_PROF_OPER);
  INSERT INTO TSYS_USER_PROFILE VALUES (ID_USER_CONS, ID_PROF_CONS);

END;