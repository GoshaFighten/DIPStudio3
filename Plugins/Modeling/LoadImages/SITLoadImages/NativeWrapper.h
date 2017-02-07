#pragma once
#include "c_types.h"
#include "prog.h"

class NativeWrapper
{
public:
	static void CallPlugin(
		int               action,
		double            step,
		double            modeltime,
		void**            ext_vars_addr,
		double*           din_vars,
		double*           derivates,
		double*           alg_vars,
		double*           alg_funcs,
		t_state_vars*     state_vars,
		t_consts*         consts,
		t_local*          locals,
		solver_struct*    solver_data,
		void*             algo_object_id
	);
};

