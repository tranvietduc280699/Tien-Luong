<template>
  <div class="selectWrapper" @click.stop="onSelect">
    <!--  hiển thị  -->
    <div class="selectBox" tabindex="0" :class="{ active: props.error }">
      <div class="Box">
        <div class="text_label">
          <span>{{ ShowLabel }}</span>
        </div>
        <div class="iconDown">
          <div class="iconDown_selectbox"></div>
        </div>
      </div>
    </div>
    <!-- hiển thị validate -->
    <div
      class="error-text"
      style="color: #f7453b; font-size: 12px"
      v-if="error"
    >
      {{ error }}
    </div>
    <!--  dropdown -->
    <div class="selectItems" v-if="close">
      <div style="padding: 8px 6px">
        <div
          class="boxItems"
          v-for="item in normalizedItems"
          :key="item.value"
          @click="selectPageSize(item)"
          :class="{ ClassActive: item.value === modelValue }"
        >
          <div class="options">
            <div
              :class="{ NumberActive: item.value === modelValue }"
              class="textLabel"
            >
              {{ item.label }}
            </div>
            <div class="iconBox">
              <div class="iconTick" v-if="item.value === modelValue"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, nextTick } from "vue";

const close = ref(false);
const onSelect = () => {
  if (!close.value) {
    close.value = true;
  } else {
    close.value = false;
  }
};
/// nhận dữ liệu từ cha
const props = defineProps({
  items: Array,
  modelValue: [String, Number, Object, null],
  error: String,
});
// gửi dữ liệu lên cha
const emit = defineEmits(["update:modelValue"]);
const selectPageSize = (item) => {
  emit("update:modelValue", item.value);
};
// chuẩn hóa number --> object
const normalizedItems = computed(() => {
  if (props.items.length > 0 && typeof props.items[0] === "number") {
    return props.items.map((n) => ({
      label: String(n),
      value: n,
    }));
  }
  return props.items;
});

// chuẩn hóa modelValue
const normalizedModelValue = computed(() => {
  if (props.modelValue === null) return null;

  // TH1: Là object → lấy value
  if (typeof props.modelValue === "Object" && props.modelValue !== undefined) {
    props.modelValue = modelValue.value;
  }
  // TH2: Là string số → convert sang number
  if (typeof props.modelValue === "String" && !isNaN(props.modelValue)) {
    props.modelValue = Number(props.modelValue);
  }
  // Kiểm tra tồn tại trong items
  const exists = normalizedItems.value.some(
    (e) => e.value === props.modelValue,
  );
  return exists ? props.modelValue : null;
});

// hiển thị label
const ShowLabel = computed(() => {
  const found = normalizedItems.value.find(
    (e) => e.value === normalizedModelValue.value,
  );
  return found ? found.label : "";
});
</script>
<style scoped lang="scss">
.selectWrapper {
  position: relative;
  // hiển thị
  .selectBox {
    display: flex;
    justify-items: center;
    align-items: center;
    background-color: #fff;
    cursor: pointer;
    border: 1px solid #e8eaec;
    position: relative;

    .Box {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0px 8px 0px 12px;

      .iconDown {
        width: 24px;
        height: 24px;
        margin-left: 4px;
        display: flex;
        justify-content: center;
        align-items: center;
      }
      .text_label {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 36px;
      }
    }
  }
  .selectBox.active {
    border: 1px solid #f7453b;
  }
  // hiển thị lỗi validate
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
  // focus
  .selectBox:focus-within {
    border: 1px solid #34b057;
  }
  // hover
  .selectBox:hover {
    border: 1px solid #34b057;
  }
  // lựa chọn ( ẩn)
  .selectItems {
    position: absolute;
    top: auto;
    margin-bottom: 6px;
    width: 100%;
    z-index: 991 !important;
    border: 1px solid #e0e0e0;
    border-radius: 4px;
    background: #fff;

    .boxItems {
      height: 37px;
      border-radius: 4px;
      font-weight: 400 !important;
      font-size: 14px;
      overflow: hidden;
      cursor: pointer;

      .options {
        height: 37px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0px 8px;
        .iconBox {
          margin-left: 8px;
          height: 24px;
          width: 24px;
          display: flex;
          justify-content: center;
          align-items: center;
        }
        .textLabel {
          line-height: 37px;
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
          flex: 1;
          min-width: 0;
        }
      }
    }

    .boxItems:hover .options {
      background: #e7f6ef;
    }
  }
  //
  .boxItems.ClassActive {
    background-color: #e7f6ef;
  }

  .NumberActive {
    height: 37px;
    color: #34b057;
  }
}
</style>
