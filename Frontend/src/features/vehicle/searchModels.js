export default function searchModels(vehicleReducer) {
  const { filter, models } = vehicleReducer;
  if (filter === null) {
    return models;
  }
  return models.filter(
    (model) => model.name.toLowerCase().indexOf(filter.toLowerCase()) > -1,
  );
}
