<template>
  <div class="selectWrapper" @click="onSelect">
    <div class="selectBox">
      <div class="Box">
        <div class="text_label">
          <span>{{ modelValue }}</span>
        </div>
        <div class="iconDown">
          <div class="iconDown_selectbox"></div>
        </div>
      </div>
    </div>

    <div class="selectItems" v-if="isOpen">
      <div style="padding: 8px 6px">
        <div
          class="boxItems"
          v-for="item in items"
          :key="item"
          @click.stop="selectPageSize(item)"
          :class="{ ClassActive: item === modelValue }"
        >
          <div class="options">
            <div :class="{ NumberActive: item === modelValue }">
              {{ item }}
            </div>
            <div class="iconBox">
              <div class="iconTick" v-if="item === modelValue"></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
// mở select items
const isOpen = ref(false);
const onSelect = () => {
  isOpen.value = !isOpen.value;
};

// nhận từ cha
const props = defineProps({
  items: {
    type: Array,
    default: () => [],
  },
  modelValue: [Number, String],
});
// gửi lên cha
const emit = defineEmits(["update:modelValue"]);
const selectPageSize = (item) => {
  emit("update:modelValue", item);
  isOpen.value = false; // Đóng dropdown sau khi chọn
};
</script>

<style scoped lang="scss">
.selectWrapper {
  position: relative;
  .selectBox {
    display: flex;
    align-items: center;
    height: 36px;
    width: 80px;
    background-color: #fff;
    border-radius: 4px;
    cursor: pointer;
    border: 1.5px solid #e8eaec;
    .Box {
      display: flex;
      justify-content: space-between;
      align-items: center;
      width: 100%;
      padding: 0 8px 0 12px;
      .text_label {
        display: flex;
        align-items: center;
        height: 36px;
      }
    }
  }

  .selectItems {
    position: absolute;
    left: 0;
    right: 0;
    bottom: 100%;
    margin-bottom: 6px;
    z-index: 991;
    border: 1.5px solid #e0e0e0;
    border-radius: 4px;
    background: #fff;
    .boxItems {
      height: 37px;
      border-radius: 4px;
      font-size: 14px;
      cursor: pointer;
      .options {
        height: 37px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 8px;
      }
      &:hover .options {
        background: #e7f6ef;
      }
    }
    .ClassActive {
      background-color: #e7f6ef;
    }
  }

  .NumberActive {
    color: #34b057;
    font-weight: bold;
  }
}
</style>
