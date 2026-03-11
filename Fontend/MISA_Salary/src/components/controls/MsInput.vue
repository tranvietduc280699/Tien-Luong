<template>
  <div class="msInput">
    <div :class="{ active: error }" class="input-text">
      <input
        ref="inputRef"
        :value="modelValue"
        type="text"
        :placeholder="placeholder"
        @input="updateValue"
      />
    </div>
    <!-- bắn lỗi validate -->
    <div
      class="error-text"
      style="color: #f7453b; font-size: 12px"
      v-if="error"
    >
      {{ error }}
    </div>
  </div>
</template>
<script setup>
import { ref } from "vue";
const props = defineProps({
  placeholder: {
    type: String,
    default: "",
  },
  modelValue: String,
  error: String,
});
const emit = defineEmits(["update:modelValue"]);
const updateValue = (e) => {
  emit("update:modelValue", e.target.value);
};

///
/// focus
///
const inputRef = ref(null);
defineExpose({
  focus: () => {
    inputRef.value?.focus();
  },
});
</script>
<style scoped lang="scss">
.active {
  border: 1px solid #f7453b !important;
}
.msInput {
  position: relative;

  .input-text {
    height: 36px;
    width: 676px;
    border-radius: 4px;
    background: #fff;
    min-height: 28px;
    align-items: center;
    overflow: hidden;
    border: 1px solid #e0e0e0;
    position: relative;
    cursor: pointer;

    input {
      height: 100%;
      border: none;
      width: 100%;
      padding: 0 12px;
    }
  }
  .input-text:focus-within {
    border: 1px solid #34b057;
  }
  // style lỗi validate
  .error-text {
    color: #f7453b;
    font-size: 12px;
    position: absolute;
    top: 100%;
    left: 0;
    margin-top: 2px;
    white-space: nowrap;
    z-index: 1;
  }
}
.input-text:hover {
  border-color: #34b057;
}
</style>
