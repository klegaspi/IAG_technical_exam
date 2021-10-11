export default async function fetchVehicleAsync() {
  const response = await fetch(
    'https://localhost:44387/vehicle-checks/makes/lotus',
  );
  // To ensure the fetchVehicle.rejected reducer is called,
  // you should check Response.ok after the Promise resolves:
  if (!response.ok) {
    throw Error();
  }
  const vehicle = await response.json();
  return vehicle.models;
}
