#include "NativeWrapper.h"
#include "math.h"
#using <mscorlib.dll>
#using <LoadImagesWrapper.dll>

void NativeWrapper::CallPlugin(
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
)
{
	switch (action) {
	case f_Stop: {
		/* Index=2
		UID=2
		GeneratorClassName=TLanguage
		Name=LangBlock13
		Type=язык программировани¤ */

	v2_sfinal__0:
		;
	}; break;
	case f_GetDeri: {
	}; break;
	case f_GetAlgFun: {
	}; break;
	default: {
		CSPlugin::InputObject^ args = gcnew CSPlugin::InputObject();
		args->Input = input_0;
		args->W = property_w;
		CSPlugin::OutputObject^ result = CSPlugin::Plugin::Run(args);
		state_vars->mydllv2_out_0_ = result->Result;
	v2___1:
		;

		/* Index=3
		UID=3
		GeneratorClassName=TOutPin
		Name=OutPin7
		Type=¬ыходной контакт s3 */

		if (isfinite(state_vars->mydllv2_out_0_)) {
			out_0 = state_vars->mydllv2_out_0_;
		};
	}; break;
	};
}
