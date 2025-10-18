import { type ReactNode } from 'react'
import { Label } from '@/components/ui/label'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'

interface FormFieldProps {
  label: string
  name: string
  type?: 'text' | 'number' | 'email' | 'password' | 'textarea'
  value: string | number
  onChange: (value: string | number) => void
  error?: string
  placeholder?: string
  required?: boolean
  disabled?: boolean
  children?: ReactNode
}

export function FormField({
  label,
  name,
  type = 'text',
  value,
  onChange,
  error,
  placeholder,
  required,
  disabled,
  children,
}: FormFieldProps) {
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    if (type === 'number') {
      onChange(parseFloat(e.target.value) || 0)
    } else {
      onChange(e.target.value)
    }
  }

  return (
    <div className="space-y-2">
      <Label htmlFor={name}>
        {label}
        {required && <span className="text-destructive ml-1">*</span>}
      </Label>
      {children ? (
        children
      ) : type === 'textarea' ? (
        <Textarea
          id={name}
          name={name}
          value={value}
          onChange={handleChange}
          placeholder={placeholder}
          disabled={disabled}
          className={error ? 'border-destructive' : ''}
        />
      ) : (
        <Input
          id={name}
          name={name}
          type={type}
          value={value}
          onChange={handleChange}
          placeholder={placeholder}
          disabled={disabled}
          className={error ? 'border-destructive' : ''}
        />
      )}
      {error && <p className="text-sm text-destructive">{error}</p>}
    </div>
  )
}
