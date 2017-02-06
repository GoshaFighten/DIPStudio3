#pragma once
/*  ------------------------------------------------------
	 Routine name:  mydll
	 Description:
	 Project file:  Код DLL.prt

------------------------------------------------------  */

/*  --- Base generator data types --- */
/* Real data type */
typedef double g_real_type;
/* Integer data type */
typedef int g_int_type;
/* Boolean data type */
typedef char g_boolean_type;
/* Complex data type */
typedef complex_64 g_complex_type;


/* Default initialization values */

/* Project signal database hash */
const unsigned int sp_database_hash_32 = 0;
/* Project sheme structure hash */
const unsigned int sp_sheme_hash_32 = 1679281830;

const double input_0_default = 0;
const double property_w_default = 0;
const double out_0_default = 0;
const double mydllv0_out_0_default = 0;
const double mydllv1_out_0_default = 0;
const double mydllv2_out_0_default = 0;

/* Main structures defines */
/* External variables count */
#define ext_vars_count 3
/* Internal state variables count */
#define state_vars_count 3
/*  --- Source model preferences --- */
/* Minimum integration step */
#define INTEGRATION_MIN_STEP 1E-5
/* Maximum integration step */
#define INTEGRATION_MAX_STEP 1
/* Integration synchronization step */
#define INTEGRATION_SYNC_STEP 0
/* Model integration method */
#define INTEGRATION_METHOD 5
/* Model relative error */
#define INTEGRATION_RELATIVE_ERROR 0.0001
/* Model absolute error */
#define INTEGRATION_ABSOLUTE_ERROR 1
/* Model end time */
#define INTEGRATION_END_TIME 10
/* Model maximum iteration count */
#define INTEGRATION_MAX_LOOP_ITER_COUNT 10


const ext_var_info_record ext_vars_names[ext_vars_count] = {
{"input:0",   vt_double,   {1}, 0,dir_input,"", (void*)&input_0_default, sizeof(double)} ,
{"property:w",   vt_double,   {1}, 1,dir_input,"", (void*)&property_w_default, sizeof(double)} ,
{"out:0",   vt_double,   {1}, 2,dir_out,"", (void*)&out_0_default, sizeof(double)}
};
#define input_0 (*(double*)(ext_vars_addr[0]))
#define property_w (*(double*)(ext_vars_addr[1]))
#define out_0 (*(double*)(ext_vars_addr[2]))

const ext_var_info_record state_vars_names[state_vars_count] = {
{"mydllv0_out_0",   vt_double,   {1}, 0, dir_inout,"Input pin state variable", (void*)&mydllv0_out_0_default, sizeof(double)} ,
{"mydllv1_out_0",   vt_double,   {1}, 8, dir_inout,"Input pin state variable", (void*)&mydllv1_out_0_default, sizeof(double)} ,
{"mydllv2_out_0",   vt_double,   {1}, 16, dir_inout,"Language out", (void*)&mydllv2_out_0_default, sizeof(double)}
};
typedef struct {
	double mydllv0_out_0_;
	double mydllv1_out_0_;
	double mydllv2_out_0_;
} t_state_vars;
typedef char t_consts;
typedef char t_local;
