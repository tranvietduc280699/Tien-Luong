import MsDropDown from "@/components/controls/MsDropDown.vue";
import MsSelectBox from "@/components/controls/MsSelectBox.vue";
import MsSelectBoxPaging from "../components/controls/MsSelectBoxPaging.vue";
import MsModal from "@/components/controls/MsModal.vue";
import MsInput from "@/components/controls/MsInput.vue";
import MsRadioBox from "@/components/controls/MsRadioBox.vue";
import MsCheckBox from "@/components/controls/MsCheckBox.vue";
import MsFormulaTextarea from "@/components/controls/MsFormulaTextarea.vue";
import MsTagBox from "../components/controls/MsTagBox.vue";
import MsModalAdd from "../components/controls/MsModalAdd.vue";
import MsTooltip from "../components/controls/MsToolTip.vue";
import MsTextarea from "../components/controls/MsTextarea.vue";
const register = (app) => {
  app.component("MsDropDown", MsDropDown);
  app.component("MsSelectBox", MsSelectBox);
  app.component("MsModal", MsModal);
  app.component("MsInput", MsInput);
  app.component("MsRadioBox", MsRadioBox);
  app.component("MsCheckBox", MsCheckBox);
  app.component("MsFormulaTextarea", MsFormulaTextarea);
  app.component("MsTagBox",MsTagBox);
  app.component("MsModalAdd",MsModalAdd);
  app.component("MsSelectBoxPaging",MsSelectBoxPaging);
  app.component("MsTooltip", MsTooltip);
  app.component("MsTextarea",MsTextarea);
};
export default {
  register,
};
