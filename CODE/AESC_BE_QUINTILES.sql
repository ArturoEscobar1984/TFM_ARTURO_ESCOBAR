SELECT 
 p.CEDULA_ALU,p.NOMBRE_ALU as first_name,p.APELLIDO_ALU+' '+p.APELLIDOM_ALU as last_name
,car.strCod_Sede,car.strCod_Fac, car.strNombre_Car
,mc.intCod_nivel,mc.strEstado_matric,mc.strTipo_matric
,mc.intRepeticion_matric,mc.decValorPagar_matric,mc.strCaso_matric
,fs.strEstructuraHogar_Ficha,fs.intFamiliares_Ficha,fs.strBdh_Ficha as BONODH,fs.strTenenciaVivienda_Ficha
,fs.strTipoVivienda_Ficha,fs.decIngresoFam_Ficha,fs.decEgresoFam_Ficha,fs.strFinanciaEstudios_Ficha
,fs.strFinanciaEstudios_Ficha
,strPersona1_Ficha as EdPadre,strActividadP1_Ficha as OcupaPadre,strPersona2_Ficha as EdMadre
,strActividadP2_Ficha as OcupaMadre
,strRespLaboral_Ficha as Trabaja
,fs.strPolitica_Ficha
,col.strNombre_col,col.strSostenimiento_col
,fs.strSBLuz_Ficha,fs.strSBAgua_Ficha,fs.strSBAlcatarillado_Ficha
,strSBTelefono_Ficha,fs.strSBInternet_Ficha,fs.strSBCable_Ficha
,fs.strPC_Ficha,fs.strTablet_Ficha,fs.strLaptop_Ficha,fs.strCellPhone_Ficha
,fs.strOtraCarrera_Ficha
,fs.strObs1_Ficha as QUINTIL
,p.FNAC_ALU
,p.OBS1_ALU as correoPersonal,OBS2_ALU as correoInstitucional
FROM AC_MATRICULAC mc
INNER JOIN SIG_PERIODOS per
ON per.strCod_per=mc.strCod_per
INNER JOIN UB_CARRERAS car
ON car.strCod_Car=per.strCod_Car 
AND car.strCod_Fac=per.strCod_Fac 
AND car.strCod_Sede=per.strCod_Sede
INNER JOIN PERSONAL p
ON p.CEDULA_ALU=mc.strCod_alu
LEFT outer JOIN BE_FSOCIOECO fs
ON fs.strCed_alu=mc.strCod_alu
LEFT OUTER JOIN DIV_PARROQUIAS prq
ON prq.strCod_prq=fs.strUbicacionEmp_Ficha
LEFT OUTER JOIN COLEGIOS col
ON col.strCod_col=fs.strCod_col
where 
--per.strEstado_per='VIGENTE'
 per.strCod_Fac<>'POSG'
AND per.strCod_Fac<>'IDIOMAS'
AND per.strCod_Fac<>'NIVELACION'
AND per.strCod_Fac<>'CFISICA'
and per.intNum_per=39
and exists (select 1 from AC_MATRICULAD md where md.strCod_matric=mc.strCod_matric) --filtra cabeceras huerfanas

ORDER BY mc.strCod_per,mc.intCod_nivel