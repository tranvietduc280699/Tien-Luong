import { reactive } from "vue";
// tạo form
export const state = reactive({
    showToast: false,
    toastSuccess :"",
    toastError : "",
    toastWarning : "",
});
let timer = null;
export const activateToast =(type, message)=>{
    // reset
    state.toastSuccess = "";
    state.toastError = "";
    state.toastWarning = "";
    //gán giá trị mới
    if (type === 'success') state.toastSuccess = message;
    if (type === 'error') state.toastError = message; 
    if (type === 'warning') state.toastWarning = message;
    state.showToast = true;
    if (timer) clearTimeout(timer);
    timer = setTimeout(()=>{
        state.showToast = false;
    },3000);
};

