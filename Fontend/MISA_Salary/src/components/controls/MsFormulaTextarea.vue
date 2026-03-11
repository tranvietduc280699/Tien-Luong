<template>
  <div class="input-text-wrapper">
    <PrismEditor
      class="my-editor"
      :model-value="modelValue"
      @update:model-value="handleInput"
      :highlight="highlighter"
      :line-numbers="false"
      spellcheck="false"
      :placeholder="placeholder"
      @keydown.tab.prevent
      @click="openDropdown"
    />
    <div class="dropdown_formula" v-if="showDropdown" ref="dropdownRef">
      <div class="dropdown">
        <div class="box_dropdown">
          <!-- phần đầu -->
          <div class="label">
            <div class="text" @click="eventFormula">
              <div :class="{ active: showFormula === true }">Công thức</div>
            </div>
            <div class="text" @click="eventParameters">
              <div :class="{ active: showParameters === true }">Tham số</div>
            </div>
          </div>
          <!-- công thức -->
          <div class="value_box" v-if="showFormula === true">
            <div
              class="value"
              v-for="(value, index) in optionFormula"
              :key="index"
              @mousedown.prevent="selectFormula(value)"
            >
              <p class="icon-formula"></p>
              <p>{{ value }}</p>
            </div>
          </div>
          <!-- giá trị -->
          <div class="value_box" v-if="showParameters === true">
            <div
              class="value"
              v-for="(item, index) in getParameters"
              :key="index"
              @mousedown.prevent="SelectionItem(item)"
            >
              <p class="icon-value"></p>
              <p>{{ item.salaryCompositionName }}</p>
              <p class="textLabel_code">({{ item.salaryCompositionCode }})</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { PrismEditor } from "vue-prism-editor";
import { onClickOutside } from "@vueuse/core";
import "vue-prism-editor/dist/prismeditor.min.css";
import Prism from "prismjs";
import { ref, watch } from "vue";
import SalaryCompositionService from "@/services/salaryCompositionAPI";
import salaryCompositionAPI from "../../services/salaryCompositionAPI";

// nhận từ cha
const props = defineProps({
  placeholder: { type: String, default: "" },
  modelValue: { type: String, default: "" },
});
// gửi lên cha
const emit = defineEmits(["update:modelValue"]);
const handleInput = (e) => {
  emit("update:modelValue", e);
};
// theo dõi để hiển thị dropdown
const showDropdown = ref(false);
watch(
  () => props.modelValue,
  async (e) => {
    if (e === "=") {
      showDropdown.value = true;
      showFormula.value = true;
    } else if (e === "" || !e.startsWith("=")) {
      showDropdown.value = false;
    }
  },
);

// phủ màu:
const formulaLanguage = {
  keyword: /\b(SUM|IF|DATE|IN|OR|MONTH)\b/gi,
  variable: [/\[.*?\]/, /\b[a-zA-ZÀ-ỹ]+\b/],
  number: /\b\d+(\.\d+)?\b/,
  operator: /[+\-*/%()=,]/,
};

const highlighter = (code) => {
  return Prism.highlight(code || "", formulaLanguage, "javascript");
};

///
/// công thức
///
const showFormula = ref(false);
const eventFormula = () => {
  showFormula.value = true;
  showParameters.value = false;
};
///
/// giá trị
///
const showParameters = ref(false);
const getParameters = ref([]);
const eventParameters = async () => {
  showParameters.value = true;
  showFormula.value = false;
  const res = await SalaryCompositionService.getAll();
  if (res && res.data) {
    getParameters.value = res.data;
  }
};
///
/// sự kiện click chọn giá trị trong công thức
///
const selectFormula = (e) => {
  if (e) {
    const valueFormula = props.modelValue + e;
    emit("update:modelValue", valueFormula);
  }
};
const SelectionItem = (e) => {
  if (e) {
    const valueFormula = props.modelValue + e.salaryCompositionCode;
    emit("update:modelValue", valueFormula);
  }
};
///
/// công thức tính toán
///
const optionFormula = {
  sum: "SUM(",
  if: "IF(",
  on: "AND(",
  or: "OR(",
  round: "ROUND(",
};
///////////
///
/// sự kiện click ngoài dropdown
///
const dropdownRef = ref(null);
onClickOutside(dropdownRef, () => {
  showDropdown.value = false;
});
const openDropdown = () => {
  showDropdown.value = true;
  showFormula.value = true;
  showParameters.value = false;
};
</script>

<style scoped lang="scss">
.input-text-wrapper {
  height: 87px;
  width: 676px;
  border-radius: 4px;
  border: 1px solid #e0e0e0;
  background: #fff;
  overflow: visible;
  display: flex;
  position: relative;

  &:hover,
  &:focus-within {
    border-color: #34b057;
  }
}
// style  trong thư viện
.my-editor {
  width: 100%;
  height: 100%;

  /* QUAN TRỌNG: dùng font không ligature để tránh lệch */
  font-family: Consolas, Monaco, monospace;
  font-size: 14px;
  line-height: 20px;

  /* ĐỒNG BỘ 2 LAYER */
  :deep(.prism-editor__textarea),
  :deep(.prism-editor__editor) {
    font-family: Consolas, Monaco, monospace !important;
    font-size: 14px !important;
    line-height: 20px !important;

    padding: 12px !important;
    margin: 0 !important;
    border: none !important;
    box-sizing: border-box !important;

    white-space: pre !important; /* Không auto wrap */
    overflow-wrap: normal !important;
    word-break: normal !important;

    font-variant-ligatures: none !important;
  }

  :deep(.prism-editor__textarea) {
    outline: none;
    caret-color: #34b057;
    z-index: 1;
  }

  :deep(.prism-editor__editor) {
    z-index: 0;
  }
}
// style dropdown
.dropdown_formula {
  height: 200px;
  max-height: 600px;
  width: 676px;
  display: flex;
  position: absolute;
  z-index: 999;
  bottom: 90px;
  border-radius: 4px;
  border: 1px solid #e0e0e0;
  transition: 0.5s;
  background-color: #fff;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);

  .dropdown {
    display: flex;
    padding: 8px;
    height: 100%;
    width: 100%;
    .box_dropdown {
      display: flex;
      padding: 16px;
      height: 100%;
      width: 100%;
      background-color: #fff;
      display: flex;
      flex-direction: column;

      // header
      .label {
        display: flex;
        border-bottom: 1px solid #9a9da0;
        position: relative;

        .text {
          font-size: 14px;
          margin-right: 24px;
          padding-bottom: 4px;
          cursor: pointer;
          position: relative;
        }
      }
      // content
      .value_box {
        display: flex;
        flex-direction: column;
        overflow-y: auto;
        flex: 1;
        min-height: 0;

        .value {
          height: 57px;
          display: flex;
          min-height: 57px;
          flex-shrink: 0;
          justify-items: center;
          align-items: center;
          cursor: pointer;
          border-radius: 4px;
          padding-left: 5px;
          flex-shrink: 0;
          box-sizing: border-box;
          transition: background-color 0.2s;
          font-weight: 700;
          color: rgb(34, 33, 33);
          &:hover {
            background-color: #e7f6ef;

            p {
              color: #34b057;
            }
          }
        }
        // mã thành phần trong tham số
        .textLabel_code {
          font-weight: normal !important;
          color: #191a1a;
          margin-left: 4px;
        }
        .value:hover {
          background-color: #e7f6ef;
        }
        // icon
        .icon-value {
          margin-right: 12px;
        }
        .icon-formula {
          margin-right: 12px;
        }
      }
    }
  }
}
//màu khi chọn
.active {
  font-weight: 700;
  color: #34b057;
  position: relative;

  &::after {
    content: "";
    position: absolute;
    left: 0;
    right: 0;
    bottom: -5px;
    height: 3px;
    background-color: #34b057;
    z-index: 1;
  }
}

///
/// style màu font chữ nhập trong thư viện
///
:deep(.token.keyword) {
  color: black;
  font-weight: 500;
}

:deep(.token.variable) {
  color: blue;
  font-weight: 500;
}

:deep(.token.operator) {
  color: red;
  font-weight: 500;
}

:deep(.token.number) {
  color: black;
  font-weight: 500;
}
:deep(.prism-editor__textarea::placeholder) {
  letter-spacing: normal !important;
  font-family: Consolas, Monaco, monospace !important;
  font-size: 14px !important;
  line-height: 20px !important;
  opacity: 0.5 !important;
  color: black;
}
</style>
