<template>
  <div class="selectWrapper">
    <!--  hiển thị lên UI -->
    <div class="selectBox" @click="open" tabindex="0">
      <div class="Box">
        <!--  hiển thị khi v-model = [] -->
        <div
          class="textAllUnit"
          v-if="!props.modelValue || props.modelValue.length === 0"
        >
          Tất cả đơn vị
        </div>
        <div class="textLabel">
          <!-- v-if="modelValue && modelValue.length > 0" -->
          <div class="delete" v-for="item in selectedLabel" :key="item.id">
            <div class="item-text">{{ item.label }}</div>
            <div class="boxIcon" @click.stop="removeSingleItem(item.id)">
              <div class="iconCloseLabel"></div>
            </div>
          </div>
        </div>
        <div class="iconDown">
          <div class="iconDown_selectbox"></div>
        </div>
      </div>
    </div>
    <!--  dropodown -->
    <div class="dropdown-panel" v-show="close" ref="dropdownRef">
      <div style="padding: 12px">
        <!-- ô tìm kiếm (input)-->
        <div class="findSelection">
          <div class="input-search-options">
            <div class="icon-search-options-box" style="padding-left: 8px">
              <div class="icon-scale-search"></div>
            </div>
            <input
              @click.stop
              v-model="searchText"
              type="text"
              placeholder="Tìm kiếm"
            />
          </div>
        </div>
        <DxTreeView
          ref="treeview"
          :items="props.items"
          key-expr="id"
          parent-id-expr="parentId"
          display-expr="label"
          data-structure="plain"
          selection-mode="multiple"
          show-check-boxes-mode="normal"
          :search-enabled="false"
          :search-value="searchText"
          :select-nodes-recursively="true"
          :check-recursive="true"
          :selected-item-keys="props.modelValue"
          @selection-changed="onSelectionChanged"
          @item-click="onItemClick"
          select-by-click="true"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch, nextTick } from "vue";
import DxTreeView from "devextreme-vue/tree-view";
import { onClickOutside } from "@vueuse/core";

// đóng or mở tree
const close = ref(false);
const open = () => {
  close.value = !close.value;
};
/// nhận dữ liệu từ cha
const props = defineProps({
  items: {
    type: Array,
    default: () => [],
  },
  modelValue: {
    type: Array,
    default: () => [],
  },
});
// gửi lên cha
const emit = defineEmits(["update:modelValue", "itemClick", "changeData"]);
const onSelectionChanged = (e) => {
  //lấy mảng id đã được chọn
  const allSelectedIds = e.component.getSelectedNodeKeys();
  // lấy object
  const allSelectedObject = e.component.getSelectedNodes();
  // lấy tất cả id và name để lưu
  const listIdAndName = allSelectedObject.map((item) => ({
    organizationUnitId: item.key,
    organizationUnitParentId: item.itemData.parentId,
    organizationUnitName: item.text,
  }));
  // gửi dữ liệu lên cha(addSalary)
  emit("changeData", listIdAndName);
  emit("update:modelValue", allSelectedIds);
  // gửi sự kiện click lên cha (salaryComposition)
  emit("itemClick", listIdAndName);
};

///
/// xóa
///
const treeview = ref(null);
const removeSingleItem = (id) => {
  const newValue = props.modelValue.filter((idItem) => idItem !== id);
  emit("update:modelValue", newValue);
  if (treeview.value && treeview.value.instance) {
    treeview.value.instance.unselectItem(id);
  }
};

///
/// hiển thị label có nhiều giá trị
///
const selectedLabel = computed(() => {
  if (!props.modelValue || !Array.isArray(props.modelValue)) return [];
  // lấy ob có id đã chọn
  const selectItem = props.items.filter((item) => {
    return props.modelValue.includes(item.id);
  });
  //bỏ con khi cha đc chọn
  const filteredList = selectItem.filter((item) => {
    const isParentSelected = props.modelValue.includes(item.parentId);
    return !isParentSelected;
  });
  return filteredList;
});

///
/// theo dõi để cập nhật lại sự kiện check box
///
watch(
  () => props.modelValue,
  (newItems) => {
    const hasData = newItems?.length > 0 && props.modelValue?.length > 0;

    if (hasData) {
      nextTick(() => {
        const instance = treeview.value?.instance;
        // Gọi key
        if (instance) {
          props.modelValue.forEach((key) => instance.selectItem(key));
        }
      });
    }
  },
);
// sự kiện click ra ngoài tất dropdown
// const dropdownRef = ref(null);
// onClickOutside(dropdownRef, () => {
//   close.value = false;
// });
// khi vào trang
onMounted(() => {
  if (props.modelValue && props.modelValue.length > 0) {
    selectedLabel();
  }
});
const searchText = ref("");
</script>
<style lang="scss">
// hiển thị
.selectWrapper {
  position: relative;
  /* hiển thị selectBox */
  .selectBox {
    display: flex;
    align-items: center;
    height: 36px;
    width: auto;
    background: #fff;
    border-radius: 4px;
    cursor: pointer;
    border: 1.5px solid #e8eaec;

    .Box {
      display: flex;
      justify-content: space-between;
      align-items: center;
      flex: 1;
      padding-left: 8px;
      width: 100%;
      height: 100%;
      overflow: hidden;

      .iconDown {
        width: 36px;
        height: 36px;
        display: flex;
        justify-content: center;
        align-items: center;
      }
      // hiển thị khi modelValue= []
      .textAllUnit {
        color: #76726f;
      }
    }
  }
  .selectBox:hover {
    border-color: #34b057;
  }
  /* search input */
  .findSelection {
    height: 36px;
    display: flex;
    align-items: center;
    margin-bottom: 16px;
    border-color: #34b057;

    .input-search-options {
      display: flex;
      align-items: center;
      flex: 1;
      height: 100%;
      border: 1px solid #34b057;
      border-radius: 4px;
      padding: 0 6px;

      .icon-search-options-box {
        margin-right: 8px;
      }

      input {
        flex: 1;
        height: 100%;
        border: none;
        outline: none;
        font-size: 14px;
      }
    }
  }
  /*dropdown-panel */
  .dropdown-panel {
    position: absolute;
    padding: 8px;
    top: 100%;
    left: 0;
    z-index: 999;
    width: 100%;
    height: auto;
    background: #fff;
    border: 1px solid #e0e0e0;
    border-radius: 4px;
  }
  /* label hiển thị */
  .textLabel {
    display: flex;
    align-items: center;
    border-radius: 4px;
    gap: 4px;
    padding-right: 4px;
    margin: 2px 0;
    flex: 1;
    overflow: hidden;
    white-space: nowrap;
    max-width: 100%;

    .boxIcon {
      width: 26px;
      height: 26px;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-left: 4px;
      flex-shrink: 0; /* Không cho icon X bị nhỏ đi */
    }
    .delete {
      display: flex;
      align-items: center;
      background-color: #e8eaef;
      height: 26px;
      padding: 0 4px 0 8px;
      border-radius: 4px;
      font-size: 14px;
      flex-shrink: 1; /* Cho phép item co lại khi thiếu không gian */
      min-width: 40px;
      max-width: 240px;
      .item-text {
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
    }
  }
}
.selectBox:focus-within {
  border: 1px solid #34b057;
}

///
/// style thư viện -----------
///

// style icon dấu cộng
.dx-treeview-toggle-item-visibility {
  position: relative;
  font-family: none !important;
  font-size: 0 !important;
  background-color: #fff;
  border: 3px solid #666666;
  border-radius: 4px;
  min-width: 16px !important;
  width: 16px !important;
  height: 16px !important;
  line-height: 16px !important;
  margin-right: 5px;
}
.dx-treeview-toggle-item-visibility::before {
  content: "";
  position: absolute;
  width: 8px;
  height: 2px;
  background: #666666;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.dx-treeview-toggle-item-visibility::after {
  content: "";
  position: absolute;
  width: 2px;
  height: 8px;
  background: #666666;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
/* khi mở → dấu − (ẩn gạch dọc) */
.dx-treeview-toggle-item-visibility-opened::after {
  display: none;
}
///
/// style icon dấu cộng
///
.dx-treeview-item-with-checkbox .dx-checkbox-container {
  width: 16px !important;
  height: 16px !important;
  border: 3px solid #666;
  border-radius: 4px;
  display: flex;
  align-items: center;
  justify-content: center;
  background: transparent;
}

/* khi checked */
.dx-checkbox-checked .dx-checkbox-container {
  border: none !important;
  background-color: #34b057 !important;
}

/* icon trong suốt */
.dx-checkbox-icon {
  background: transparent !important;
}

/* tick trắng */
.dx-checkbox-checked .dx-checkbox-icon::before {
  color: #fff !important;
}

.dx-checkbox-indeterminate .dx-checkbox-icon::before {
  background-color: #fff !important;
}
.dx-checkbox-icon {
  width: 16px !important;
  height: 16px !important;
}
.dx-checkbox-checked .dx-checkbox-icon {
  font: 11px / 1em DXIcons !important;
}
/* hover item */
.dx-treeview-item:hover {
  background-color: #e6f7ef !important;
}
.dx-treeview-item-with-checkbox .dx-treeview-item {
  border-radius: 4px !important;
}
//
.dx-state-selected > .dx-treeview-item {
  background-color: #e6f7ef !important;
}
</style>
