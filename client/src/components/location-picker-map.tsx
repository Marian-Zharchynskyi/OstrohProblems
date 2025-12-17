import { useState, useEffect, useMemo } from 'react'
import { MapContainer, TileLayer, Marker, useMapEvents } from 'react-leaflet'
import { Icon } from 'leaflet'
import type { LatLngBoundsExpression } from 'leaflet'
import 'leaflet/dist/leaflet.css'

interface LocationPickerMapProps {
  latitude: number
  longitude: number
  onLocationChange?: (lat: number, lng: number, streetName: string) => void
  readonly?: boolean
  height?: string
}

// Ostroh city center coordinates
const OSTROH_CENTER: [number, number] = [50.3292, 26.5143]

// Bounds for Ostroh + ~10km radius
const MAP_BOUNDS: LatLngBoundsExpression = [
  [50.2392, 26.4243],
  [50.4192, 26.6043],
]

const markerIcon = new Icon({
  iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-blue.png',
  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
})

function LocationMarker({
  position,
  onLocationChange,
  readonly,
}: {
  position: [number, number]
  onLocationChange?: (lat: number, lng: number, streetName: string) => void
  readonly?: boolean
}) {
  const [markerPosition, setMarkerPosition] = useState<[number, number]>(position)

  useEffect(() => {
    setMarkerPosition(position)
  }, [position])

  useMapEvents({
    click: async (e) => {
      if (readonly) return
      
      const { lat, lng } = e.latlng
      setMarkerPosition([lat, lng])
      
      // Reverse geocoding to get street name
      try {
        const response = await fetch(
          `https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}&zoom=18&addressdetails=1`
        )
        const data = await response.json()
        const address = data.address || {}
        const streetName = address.road || address.pedestrian || address.footway || 
                          address.street || address.suburb || address.neighbourhood || 
                          address.city || 'Невідома вулиця'
        
        onLocationChange?.(lat, lng, streetName)
      } catch {
        onLocationChange?.(lat, lng, 'Невідома вулиця')
      }
    },
  })

  return <Marker position={markerPosition} icon={markerIcon} />
}

export function LocationPickerMap({
  latitude,
  longitude,
  onLocationChange,
  readonly = false,
  height = '300px',
}: LocationPickerMapProps) {
  const hasValidCoords = latitude !== 0 && longitude !== 0
  
  const center = useMemo(() => {
    if (hasValidCoords) {
      return [latitude, longitude] as [number, number]
    }
    return OSTROH_CENTER
  }, [latitude, longitude, hasValidCoords])

  const position: [number, number] = hasValidCoords 
    ? [latitude, longitude] 
    : OSTROH_CENTER

  return (
    <div className="rounded-lg overflow-hidden border" style={{ height }}>
      <MapContainer
        center={center}
        zoom={15}
        style={{ height: '100%', width: '100%' }}
        maxBounds={MAP_BOUNDS}
        maxBoundsViscosity={1.0}
        minZoom={12}
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <LocationMarker
          position={position}
          onLocationChange={onLocationChange}
          readonly={readonly}
        />
      </MapContainer>
      {!readonly && (
        <p className="text-xs text-muted-foreground mt-2">
          Натисніть на карту, щоб вибрати місце проблеми
        </p>
      )}
    </div>
  )
}
