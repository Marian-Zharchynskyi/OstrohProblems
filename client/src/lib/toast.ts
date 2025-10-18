// Simple toast utility - can be replaced with a proper toast library like sonner or react-hot-toast
export const toast = {
  success: (message: string) => {
    console.log('✅ Success:', message)
    alert(message)
  },
  error: (message: string) => {
    console.error('❌ Error:', message)
    alert(message)
  },
  info: (message: string) => {
    console.log('ℹ️ Info:', message)
    alert(message)
  },
}
