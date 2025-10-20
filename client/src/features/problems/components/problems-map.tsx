import { useState, useMemo } from 'react'
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import { Icon } from 'leaflet'
import type { Problem } from '@/types'
import { ProblemDetailPopup } from './problem-detail-popup'
import 'leaflet/dist/leaflet.css'

interface ProblemsMapProps {
  problems: Problem[]
}

// Custom marker icon
const problemIcon = new Icon({
  iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
})

export function ProblemsMap({ problems }: ProblemsMapProps) {
  const [selectedProblem, setSelectedProblem] = useState<Problem | null>(null)

  // Calculate center of all problems
  const center = useMemo(() => {
    if (problems.length === 0) {
      // Default to Ostroh coordinates
      return { lat: 50.3294, lng: 26.5144 }
    }

    const avgLat = problems.reduce((sum, p) => sum + p.latitude, 0) / problems.length
    const avgLng = problems.reduce((sum, p) => sum + p.longitude, 0) / problems.length

    return { lat: avgLat, lng: avgLng }
  }, [problems])

  return (
    <>
      <div className="h-full w-full rounded-lg overflow-hidden shadow-lg">
        <MapContainer
          center={[center.lat, center.lng]}
          zoom={14}
          style={{ height: '100%', width: '100%' }}
          className="z-0"
        >
          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />

          {problems.map((problem) => (
            <Marker
              key={problem.id}
              position={[problem.latitude, problem.longitude]}
              icon={problemIcon}
              eventHandlers={{
                click: () => setSelectedProblem(problem),
              }}
            >
              <Popup>
                <div className="min-w-[200px]">
                  <h3 className="font-semibold text-gray-900 mb-1">{problem.title}</h3>
                  <p className="text-sm text-gray-600 mb-2 line-clamp-2">
                    {problem.description}
                  </p>
                  {problem.problemStatus && (
                    <span className="inline-flex items-center rounded-full bg-blue-50 px-2 py-1 text-xs text-blue-700">
                      {problem.problemStatus.name}
                    </span>
                  )}
                  <button
                    onClick={() => setSelectedProblem(problem)}
                    className="mt-2 w-full rounded-md bg-blue-600 px-3 py-1.5 text-sm font-medium text-white hover:bg-blue-700 transition-colors"
                  >
                    Детальніше
                  </button>
                </div>
              </Popup>
            </Marker>
          ))}
        </MapContainer>
      </div>

      {selectedProblem && (
        <ProblemDetailPopup
          problem={selectedProblem}
          onClose={() => setSelectedProblem(null)}
        />
      )}
    </>
  )
}
